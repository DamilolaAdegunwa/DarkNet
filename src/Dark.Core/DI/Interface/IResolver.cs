using System;

namespace Dark.Core.DI
{
    public interface IResolver
    {
        /// <summary>
        /// 通过接口类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        TInter Resolve<TInter>();

        /// <summary>
        /// 通过Type来解析对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// 通过名称来实现对象IService
        /// </summary>
        /// <typeparam name="TInter"></typeparam>
        /// <param name="implName"></param>
        /// <returns></returns>
        TInter Resolve<TInter>(string implName);

        void Release(object obj);
    }
}
