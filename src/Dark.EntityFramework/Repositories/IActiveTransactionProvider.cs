using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;

namespace Dark.EntityFramework.Repositories
{
    public interface IActiveTransactionProvider
    {
        /// <summary>
        ///     Gets the active transaction or null if current UOW is not transactional.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IDbTransaction GetActiveTransaction(Dictionary<string, object> args);

        /// <summary>
        ///     Gets the active database connection.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IDbConnection GetActiveConnection(Dictionary<string, object> args);
    }


    public class EfActiveTransactionProvider : IActiveTransactionProvider, ITransientDependency
    {
        private readonly IResolver _iocResolver;

        public EfActiveTransactionProvider(IResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IDbTransaction GetActiveTransaction(Dictionary<string, object> args)
        {
            return GetDbContext(args).Database.CurrentTransaction.UnderlyingTransaction;
        }

        public IDbConnection GetActiveConnection(Dictionary<string, object> args)
        {
            return GetDbContext(args).Database.Connection;
        }

        private DbContext GetDbContext(Dictionary<string, object> args)
        {
            var dbContextProviderType = typeof(IDbContextProvider<>).MakeGenericType((Type)args["ContextType"]);

            try
            {
                var dbContextProviderWrapper = _iocResolver.Resolve(dbContextProviderType);
                var method = dbContextProviderWrapper.GetType()
                   .GetMethod(
                       nameof(IDbContextProvider<BaseDbContext>.GetDbContext)
                   );

                return (DbContext)method.Invoke(
                    dbContextProviderWrapper, null
                );
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _iocResolver.Release(dbContextProviderType);
            }

        }
    }
}
