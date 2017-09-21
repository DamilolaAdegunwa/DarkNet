using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.DI
{

    /// <summary>
    /// This interface is used to wrap an object that is resolved from IOC container.
    /// It inherits <see cref="IDisposable"/>, so resolved object can be easily released.
    /// In <see cref="IDisposable.Dispose"/> method, <see cref="IIocResolver.Release"/> is called to dispose the object.
    /// This is non-generic version of <see cref="IDisposableDependencyObjectWrapper{T}"/> interface.
    /// </summary>
    public interface IDisposableDependencyObjectWrapper : IDisposableDependencyObjectWrapper<object>
    {

    }

    /// <summary>
    /// This interface is used to wrap an object that is resolved from IOC container.
    /// It inherits <see cref="IDisposable"/>, so resolved object can be easily released.
    /// In <see cref="IDisposable.Dispose"/> method, <see cref="IIocResolver.Release"/> is called to dispose the object.
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    public interface IDisposableDependencyObjectWrapper<out T> : IDisposable
    {
        /// <summary>
        /// The resolved object.
        /// </summary>
        T Object { get; }
    }

    internal class DisposableDependencyObjectWrapper : DisposableDependencyObjectWrapper<object>,
        IDisposableDependencyObjectWrapper
    {
        public DisposableDependencyObjectWrapper(IResolver iocResolver, object obj)
            : base(iocResolver, obj)
        {

        }
    }

    internal class DisposableDependencyObjectWrapper<T> : IDisposableDependencyObjectWrapper<T>
    {
        private readonly IResolver _iocResolver;

        public T Object { get; private set; }

        public DisposableDependencyObjectWrapper(IResolver iocResolver, T obj)
        {
            _iocResolver = iocResolver;
            Object = obj;
        }

        public void Dispose()
        {
            _iocResolver.Release(Object);
        }
    }
}
