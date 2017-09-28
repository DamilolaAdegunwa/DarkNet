using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Dark.Core.DI;
using Dark.Core.Domain.Entity;
using Dark.Core.Domain.Repository;
using Dark.EntityFramework.Repositories;

namespace Dark.EntityFramework.Common
{
    public interface IEfGenericRepositoryRegistrar
    {
        void RegisterForDbContext(Type dbContextType, IIocManager iocManager);
    }

    public class EfGenericRepositoryRegistrar : IEfGenericRepositoryRegistrar, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IDbContextEntityFinder _dbContextEntityFinder;

        public EfGenericRepositoryRegistrar(IDbContextEntityFinder dbContextEntityFinder)
        {
            _dbContextEntityFinder = dbContextEntityFinder;
            Logger = NullLogger.Instance;
        }

        public void RegisterForDbContext(
            Type dbContextType,
            IIocManager iocManager)
        {
            //var autoRepositoryAttr = dbContextType.GetTypeInfo().GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>() ?? defaultAutoRepositoryTypesAttribute;

            RegisterForDbContext(
                dbContextType,
                iocManager,
                typeof(IRepository<>),
                typeof(IRepository<,>),
                typeof(EfRepository<,>),
                typeof(EfRepository<,,>)
            );

            //if (autoRepositoryAttr.WithDefaultRepositoryInterfaces)
            //{
            //    RegisterForDbContext(
            //        dbContextType,
            //        iocManager,
            //        defaultAutoRepositoryTypesAttribute.RepositoryInterface,
            //        defaultAutoRepositoryTypesAttribute.RepositoryInterfaceWithPrimaryKey,
            //        autoRepositoryAttr.RepositoryImplementation,
            //        autoRepositoryAttr.RepositoryImplementationWithPrimaryKey
            //    );
            //}
        }

        /// <summary>
        /// 用于注册dbContext的所有实体对象
        /// </summary>
        /// <param name="dbContextType"></param>
        /// <param name="iocManager"></param>
        /// <param name="repositoryInterface"></param>
        /// <param name="repositoryInterfaceWithPrimaryKey"></param>
        /// <param name="repositoryImplementation"></param>
        /// <param name="repositoryImplementationWithPrimaryKey"></param>
        private void RegisterForDbContext(
            Type dbContextType,
            IIocManager iocManager,
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation,
            Type repositoryImplementationWithPrimaryKey)
        {
            IEnumerable<EntityTypeInfo> entityTypes = _dbContextEntityFinder.GetEntityTypeInfos(dbContextType);
            foreach (var entityTypeInfo in entityTypes)
            {
                var primaryKeyType = EntityHelper.GetPrimaryKeyType(entityTypeInfo.EntityType);
                if (primaryKeyType == typeof(int))
                {
                    var genericRepositoryType = repositoryInterface.MakeGenericType(entityTypeInfo.EntityType);
                    if (!iocManager.IsRegistered(genericRepositoryType))
                    {
                        var implType = repositoryImplementation.GetGenericArguments().Length == 1
                            ? repositoryImplementation.MakeGenericType(entityTypeInfo.EntityType)
                            : repositoryImplementation.MakeGenericType(entityTypeInfo.DeclaringType,
                                entityTypeInfo.EntityType);

                        
                        iocManager.IocContainer.Register(
                            Component
                                .For(genericRepositoryType)
                                .ImplementedBy(implType)
                                //.Named(Guid.NewGuid().ToString("N"))
                                .LifestyleTransient()
                        );
                    }
                }

                var genericRepositoryTypeWithPrimaryKey = repositoryInterfaceWithPrimaryKey.MakeGenericType(entityTypeInfo.EntityType, primaryKeyType);
                if (!iocManager.IsRegistered(genericRepositoryTypeWithPrimaryKey))
                {
                    var implType = repositoryImplementationWithPrimaryKey.GetGenericArguments().Length == 2
                        ? repositoryImplementationWithPrimaryKey.MakeGenericType(entityTypeInfo.EntityType, primaryKeyType)
                        : repositoryImplementationWithPrimaryKey.MakeGenericType(entityTypeInfo.DeclaringType, entityTypeInfo.EntityType, primaryKeyType);


                    var keyName = "PK_" + primaryKeyType.Name + "_" + entityTypeInfo.EntityType.Name;

                    iocManager.IocContainer.Register(
                        Component
                            .For(genericRepositoryTypeWithPrimaryKey)
                            .ImplementedBy(implType)
                            .Named(keyName)
                            .LifestyleTransient()
                    );
                }
            }
        }
    }

}
