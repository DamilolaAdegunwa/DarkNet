using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.DI
{
    public class IocManager : IIocManager
    {
        #region 1.0 IIocManager

        public static IocManager Instance { get; private set; }

        /// <summary>
        /// Reference to the Castle Windsor Container.
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        public List<IRegisterConvention> _registerConventions { get; }


        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        /// 实现多线程安全
        /// </summary>
        public IocManager()
        {
            //初始化
            IocContainer = new WindsorContainer();
            //多服务对应一个对象
            IocContainer.Register(
                Component.For<IIocManager, IResolver, IRegister, IocManager>()
                .UsingFactoryMethod(() => this)
                .LifestyleSingleton());
            //加载记录的程序集
            _registerConventions = new List<IRegisterConvention>();
        }



        /// <summary>
        /// 释放组件
        /// </summary>
        /// <param name="obj"></param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <summary>
        /// 释放容器 
        /// </summary>
        public void Dispose()
        {
            IocContainer.Dispose();
        }

        /// <summary>
        /// 检查是否已经注册了
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        public bool IsRegistered<TInter>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TInter));
        }

        #endregion

        #region 2.0 IRegister

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="type"></param>
        public void Register(Type type, DependencyLife life)
        {
            switch (life)
            {
                case DependencyLife.Singleton:
                    IocContainer.Register(
                          Component.For(type)
                          .ImplementedBy(type)
                          .LifestyleSingleton());
                    break;
                case DependencyLife.Transient:
                    IocContainer.Register(
                          Component.For(type)
                          .ImplementedBy(type)
                          .LifestyleTransient());
                    break;
            }

        }


        /// <summary>
        /// 检查是否有注册,如果注册过,就不在注册
        /// </summary>
        /// <param name="type"></param>
        public void RegiserIfNot(Type type)
        {
            if (!IsRegistered(type))
            {
                Register(type, DependencyLife.Singleton);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInter"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        public void Register<TInter, TImpl>() where TInter : class
            where TImpl : TInter
        {
            IocContainer.Register(
                Component.For<TInter>()
                .ImplementedBy<TImpl>()
                .LifestyleTransient());
        }

        public void Register<TInter, TImpl>(DependencyLife life = DependencyLife.Transient) where TInter : class
            where TImpl : TInter
        {
            switch (life)
            {
                case DependencyLife.Transient:

                    IocContainer.Register(
                         Component.For<TInter>()
                         .ImplementedBy<TImpl>()
                         .LifestyleTransient()
                    );
                    break;
                case DependencyLife.Singleton:
                    IocContainer.Register(
                         Component.For<TInter>()
                         .ImplementedBy<TImpl>()
                         .LifestyleSingleton()
                    );
                    break;
            }
        }

        public void Register<TInter, TImpl>(string implName)
            where TInter : class
            where TImpl : TInter
        {
            IocContainer.Register(
                Component.For<TInter>()
                .ImplementedBy<TImpl>().Named(implName)
                .LifestyleTransient());
        }


        public void AddRegisterConvention(IRegisterConvention rConvertion)
        {
            _registerConventions.Add(rConvertion);
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="assemblies"></param>
        public void RegisterConvention(Assembly assembly)
        {
            var context = new RegisterContext(this, assembly, true);
            foreach (var register in _registerConventions)
            {
                register.RegisterAssembly(context);
            }
        }

        /// <summary>
        /// 安装
        /// </summary>
        /// <param name="installers"></param>
        public void Install(IWindsorInstaller[] installers)
        {
            IocContainer.Install(installers);
        }

        #endregion

        #region 3.0 IResolver

        public TInter Resolve<TInter>()
        {
            return IocContainer.Resolve<TInter>();
        }

        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        public TInter Resolve<TInter>(string implName)
        {
            return IocContainer.Resolve<TInter>(implName);
        }

        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }


        ///<inheritdoc/>
        public T[] ResolveAll<T>()
        {
            return IocContainer.ResolveAll<T>();
        }

        ///<inheritdoc/>
        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll<T>(argumentsAsAnonymousType);
        }

        ///<inheritdoc/>
        public object[] ResolveAll(Type type)
        {
            return IocContainer.ResolveAll(type).Cast<object>().ToArray();
        }

        ///<inheritdoc/>
        public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll(type, argumentsAsAnonymousType).Cast<object>().ToArray();
        }

        #endregion

    }
}
