﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Dark.Core;
using Dark.Core.DI;
using Dark.Core.Domain.Uow;
using Dark.Core.Extension;
using Dark.Core.Modules;
using Dark.Core.Reflections;
using Dark.EntityFramework.Common;
using Dark.EntityFramework.Uow;

namespace Dark.EntityFramework
{
    [DependOn(typeof(CoreModule))]
    public class EntityFrameworkModule : BaseModule
    {
        private readonly ITypeFinder _typeFinder;

        public EntityFrameworkModule(ITypeFinder typeFinder)
        {
            this._typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {

            Configuration.ReplaceService(typeof(IUnitOfWorkFilterExecuter), () =>
             {
                 IocManager.IocContainer.Register(
                     Component
                     .For<IUnitOfWorkFilterExecuter, IEfUnitOfWorkFilterExecuter>()
                     .ImplementedBy<EfDynamicFiltersUnitOfWorkFilterExecuter>()
                     .LifestyleTransient()
                 );



                 //IocManager.IocContainer.Register<IEfTransactionStrategy, DbContextEfTransactionStrategy>(DependencyLifeStyle.Transient);

             });
        }

        public override void Initialize()
        {

            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
               Component.For(typeof(IDbContextProvider<>))
                   .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                   .LifestyleTransient()
               );


            RegisterGenericRepositoriesAndMatchDbContexes();
        }


        private void RegisterGenericRepositoriesAndMatchDbContexes()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic 
                    && !type.IsAbstract
                    && type.IsClass
                    && (typeof(BaseDbContext).IsAssignableFrom(type))
                    );

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from BaseDbContext.");
                return;
            }

            using (var scope = IocManager.CreateScope())
            {
                var repositoryRegistrar = scope.Resolve<IEfGenericRepositoryRegistrar>();

                //给所有dbContext 的IDbSet对象的实体注册到IRepository中
                foreach (var dbContextType in dbContextTypes)
                {
                    Logger.Info("Registering DbContext: " + dbContextType.AssemblyQualifiedName);
                    repositoryRegistrar.RegisterForDbContext(dbContextType, IocManager);
                    
                }

                scope.Resolve<IDbContextTypeMatcher>().Populate(dbContextTypes);
            }
        }
    }
}
