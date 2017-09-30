using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Creator { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }


        public EntityBase()
        {
            this.CreateTime = DateTime.Now;
        }
    }


    /// <summary>
    /// 默认的Id是 int 类型 
    /// </summary>
    public class Entity : Entity<int>
    {

    }

    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        [Required]
        public virtual T Id { get; set; }
        /// <summary>
        /// 用于建从Id 键是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTransient()
        {
            if (EqualityComparer<T>.Default.Equals(Id, default(T)))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(T) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(T) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }
    }

    /// <summary>
    /// 泛型的Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }

        bool IsTransient();
    }
}
