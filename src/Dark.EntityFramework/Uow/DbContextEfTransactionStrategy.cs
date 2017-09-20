using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dark.Core.DI;
using Dark.Core.Domain.Uow;
using Dark.Core.Extension;

namespace Dark.EntityFramework.Uow
{

    #region 1.0 接口

    public interface IEfTransactionStrategy
    {
        void InitOptions(UnitOfWorkOptions options);

        DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver)
            where TDbContext : DbContext;

        void Commit();

        void Dispose(IResolver iocResolver);
    }

    #endregion


    #region 2.0 具体实现
    public class DbContextEfTransactionStrategy : 
        IEfTransactionStrategy,
        //默认实现该
        ITransientDependency
    {
        protected UnitOfWorkOptions Options { get; private set; }

        protected IDictionary<string, ActiveTransactionInfo> ActiveTransactions { get; }

        public DbContextEfTransactionStrategy()
        {
            ActiveTransactions = new Dictionary<string, ActiveTransactionInfo>();
        }

        public void InitOptions(UnitOfWorkOptions options)
        {
            Options = options;
        }


        public void Commit()
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbContextTransaction.Commit();
            }
        }

        public DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver) where TDbContext : DbContext
        {
            DbContext dbContext = null;

            var activeTransaction = ActiveTransactions.GetOrDefault(connectionString);
            if (activeTransaction == null)
            {
                dbContext = dbContextResolver.Resolve<TDbContext>(connectionString);

                var dbtransaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                activeTransaction = new ActiveTransactionInfo(dbtransaction, dbContext);

                ActiveTransactions[connectionString] = activeTransaction;
            }
            else
            {
                dbContext = dbContextResolver.Resolve<TDbContext>(activeTransaction.DbContextTransaction.UnderlyingTransaction.Connection, false);
                dbContext.Database.UseTransaction(activeTransaction.DbContextTransaction.UnderlyingTransaction);
                activeTransaction.AttendedDbContexts.Add(dbContext);
            }

            return dbContext;
        }

        public void Dispose(IResolver iocResolver)
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                foreach (var attendedDbContext in activeTransaction.AttendedDbContexts)
                {
                    iocResolver.Release(attendedDbContext);
                }

                activeTransaction.DbContextTransaction.Dispose();
                iocResolver.Release(activeTransaction.StarterDbContext);
            }

            ActiveTransactions.Clear();
        }
    } 
    #endregion


    public static class IsolationLevelExtensions
    {
        /// <summary>
        /// Converts <see cref="System.Transactions.IsolationLevel"/> to <see cref="IsolationLevel"/>.
        /// </summary>
        public static System.Data.IsolationLevel ToSystemDataIsolationLevel(this System.Transactions.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case System.Transactions.IsolationLevel.Chaos:
                    return System.Data.IsolationLevel.Chaos;
                case System.Transactions.IsolationLevel.ReadCommitted:
                    return System.Data.IsolationLevel.ReadCommitted;
                case System.Transactions.IsolationLevel.ReadUncommitted:
                    return System.Data.IsolationLevel.ReadUncommitted;
                case System.Transactions.IsolationLevel.RepeatableRead:
                    return System.Data.IsolationLevel.RepeatableRead;
                case System.Transactions.IsolationLevel.Serializable:
                    return System.Data.IsolationLevel.Serializable;
                case System.Transactions.IsolationLevel.Snapshot:
                    return System.Data.IsolationLevel.Snapshot;
                case System.Transactions.IsolationLevel.Unspecified:
                    return System.Data.IsolationLevel.Unspecified;
                default:
                    throw new Exception("Unknown isolation level: " + isolationLevel);
            }
        }
    }
}



