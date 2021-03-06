﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;
using Dark.Core.Domain.Uow;
using Dark.Core.Extension;

namespace Dark.EntityFramework
{

    public interface IDbContextTypeMatcher
    {
        void Populate(Type[] dbContextTypes);

        Type GetConcreteType(Type sourceDbContextType);
    }

    public class DbContextTypeMatcher : IDbContextTypeMatcher, ISingletonDependency
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly Dictionary<Type, List<Type>> _dbContextTypes;

        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _dbContextTypes = new Dictionary<Type, List<Type>>();
        }

        public void Populate(Type[] dbContextTypes)
        {
            foreach (var dbContextType in dbContextTypes)
            {
                var types = new List<Type>();

                AddWithBaseTypes(dbContextType, types);

                foreach (var type in types)
                {
                    Add(type, dbContextType);
                }
            }
        }

        //TODO: GetConcreteType method can be optimized by extracting/caching MultiTenancySideAttribute attributes for DbContexes.

        public virtual Type GetConcreteType(Type sourceDbContextType)
        {
            //TODO: This can also get MultiTenancySide to filter dbcontexes

            if (!sourceDbContextType.GetTypeInfo().IsAbstract)
            {
                return sourceDbContextType;
            }

            //Get possible concrete types for given DbContext type
            var allTargetTypes = _dbContextTypes.GetOrDefault(sourceDbContextType);

            if (allTargetTypes.IsNullOrEmpty())
            {
                throw new Exception("Could not find a concrete implementation of given DbContext type: " + sourceDbContextType.AssemblyQualifiedName);
            }

            if (allTargetTypes.Count == 1)
            {
                //Only one type does exists, return it
                return allTargetTypes[0];
            }

            CheckCurrentUow();


            return GetDefaultDbContextType(allTargetTypes, sourceDbContextType);
        }

        private void CheckCurrentUow()
        {
            if (_currentUnitOfWorkProvider.Current == null)
            {
                throw new Exception("GetConcreteType method should be called in a UOW.");
            }
        }

        private static Type GetDefaultDbContextType(List<Type> dbContextTypes,Type sourceType)
        {
            if (dbContextTypes.Count == 1)
            {
                return dbContextTypes[0];
            }
            //var filteredTypes = dbContextTypes
            //    .Where(type => !type.GetTypeInfo().IsDefined(typeof(AutoRepositoryTypesAttribute), true))
            //    .ToList();

            //if (filteredTypes.Count == 1)
            //{
            //    return filteredTypes[0];
            //}

            //filteredTypes = filteredTypes
            //    .Where(type => !type.GetTypeInfo().IsDefined(typeof(DefaultDbContextAttribute), true))
            //    .ToList();

            //if (filteredTypes.Count == 1)
            //{
            //    return filteredTypes[0];
            //}

            throw new Exception(string.Format(
                "Found more than one concrete type for given DbContext Type ({0})  Found types: {1}",
                sourceType,
               string.Join(",", dbContextTypes.Select(c => c.AssemblyQualifiedName))
                ));
        }

        private static void AddWithBaseTypes(Type dbContextType, List<Type> types)
        {
            types.Add(dbContextType);
            if (dbContextType != typeof(BaseDbContext))
            {
                AddWithBaseTypes(dbContextType.GetTypeInfo().BaseType, types);
            }
        }

        private void Add(Type sourceDbContextType, Type targetDbContextType)
        {
            if (!_dbContextTypes.ContainsKey(sourceDbContextType))
            {
                _dbContextTypes[sourceDbContextType] = new List<Type>();
            }

            _dbContextTypes[sourceDbContextType].Add(targetDbContextType);
        }
    }
}
