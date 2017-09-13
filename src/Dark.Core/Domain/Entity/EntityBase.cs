using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Entity
{
    /// <summary>
    /// 业务entity 带有create
    /// </summary>
    public class EntityBase : Entity
    {

        /// <summary>
        /// 创建人
        /// </summary>
        public long Creator { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 默认的Id是 int 类型 
    /// </summary>
    public class Entity : IEntity<long>
    {
        public long Id { get; set; }
    }

    /// <summary>
    /// 泛型的Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}
