using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.DI
{
    public interface IRegister
    {


        void AddRegisterConvention(IRegisterConvention register);

        void Register(Type type, DependencyLife life = DependencyLife.Singleton);
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TInter"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="name"></param>
        void Register<TInter, TImpl>()
            where TInter : class
            where TImpl : TInter;



        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TInter"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="name"></param>
        void Register<TInter, TImpl>(DependencyLife life)
            where TInter : class
            where TImpl : TInter;
        /// <summary>
        /// 通过名字来初始化,默认是使用transient
        /// </summary>
        /// <typeparam name="TInter"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="implName"></param>
        void Register<TInter, TImpl>(string implName)
            where TInter : class
            where TImpl : TInter;



        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="assembly"></param>
        void RegisterConvention(Assembly assembly);

        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="type"></param>
        void RegiserIfNot(Type type);


        /// <summary>
        /// 安装组件
        /// </summary>
        /// <param name="installers"></param>
        void Install(IWindsorInstaller[] installers);
    }
}
