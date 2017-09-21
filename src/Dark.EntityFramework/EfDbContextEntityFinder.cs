using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;
using Dark.Core.Domain.Entity;
using Dark.Core.Utils;

namespace Dark.EntityFramework
{

    public interface IDbContextEntityFinder
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }

    public class EfDbContextEntityFinder : IDbContextEntityFinder, ITransientDependency
    {
        public IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                (ReflectionTools.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                 ReflectionTools.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) &&
                ReflectionTools.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
                select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
        }
    }
}
