﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;
using Dark.Core.Domain.Uow;
using Dark.Core.Extension;

namespace Dark.EntityFramework.Uow
{
    public class EfUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        protected IDictionary<string, DbContext> ActiveDbContexts { get; }
        protected IResolver IocResolver { get; }

        private readonly IDbContextResolver _dbContextResolver;
        private readonly IDbContextTypeMatcher _dbContextTypeMatcher;
        private readonly IEfTransactionStrategy _transactionStrategy;

        /// <summary>
        /// Creates a new <see cref="EfUnitOfWork"/>.
        /// </summary>
        public EfUnitOfWork(
            IResolver iocResolver,
            IConnectionStringResolver connectionStringResolver,
            IDbContextResolver dbContextResolver,
            IEfUnitOfWorkFilterExecuter filterExecuter,
            IUnitOfWorkDefaultOptions defaultOptions,
            IDbContextTypeMatcher dbContextTypeMatcher,
            IEfTransactionStrategy transactionStrategy)
            : base(
                  connectionStringResolver,
                  defaultOptions,
                  filterExecuter)
        {
            IocResolver = iocResolver;
            _dbContextResolver = dbContextResolver;
            _dbContextTypeMatcher = dbContextTypeMatcher;
            _transactionStrategy = transactionStrategy;

            ActiveDbContexts = new Dictionary<string, DbContext>();
        }

        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.InitOptions(Options);
            }
        }

        public override void SaveChanges()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                SaveChangesInDbContext(dbContext);
            }
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        public IReadOnlyList<DbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToList();
        }

        protected override void CompleteUow()
        {
            SaveChanges();

            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();

            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }

        /// <summary>
        /// 获取dbContext
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <returns></returns>
        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            //1:找到dbContext
            var concreteDbContextType = _dbContextTypeMatcher.GetConcreteType(typeof(TDbContext));

            var connectionString = ResolveConnectionString();

            var dbContextKey = concreteDbContextType.FullName + "#" + connectionString;

            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(dbContextKey, out dbContext))
            {
                if (DefaultOptions.IsTransactional == true)
                {
                    dbContext = _transactionStrategy.CreateDbContext<TDbContext>(connectionString, _dbContextResolver);
                }
                else
                {
                    dbContext = _dbContextResolver.Resolve<TDbContext>(connectionString);
                }

                if (DefaultOptions.Timeout.HasValue && !dbContext.Database.CommandTimeout.HasValue)
                {
                    dbContext.Database.CommandTimeout = DefaultOptions.Timeout.Value.TotalSeconds.To<int>();
                }

                ((IObjectContextAdapter)dbContext).ObjectContext.ObjectMaterialized += (sender, args) =>
                {
                    ObjectContext_ObjectMaterialized(dbContext, args);
                };

                FilterExecuter.As<IEfUnitOfWorkFilterExecuter>().ApplyCurrentFilters(this, dbContext);

                ActiveDbContexts[dbContextKey] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        protected override void DisposeUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Dispose(IocResolver);
            }
            else
            {
                foreach (var activeDbContext in GetAllActiveDbContexts())
                {
                    Release(activeDbContext);
                }
            }

            ActiveDbContexts.Clear();
        }

        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocResolver.Release(dbContext);
        }

        private static void ObjectContext_ObjectMaterialized(DbContext dbContext, ObjectMaterializedEventArgs e)
        {
            var entityType = ObjectContext.GetObjectType(e.Entity.GetType());

            dbContext.Configuration.AutoDetectChangesEnabled = false;
            var previousState = dbContext.Entry(e.Entity).State;

            //DateTimePropertyInfoHelper.NormalizeDatePropertyKinds(e.Entity, entityType);

            dbContext.Entry(e.Entity).State = previousState;
            dbContext.Configuration.AutoDetectChangesEnabled = true;
        }
      
    }
}
