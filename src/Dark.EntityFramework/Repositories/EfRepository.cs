﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Entity;

namespace Dark.EntityFramework.Repositories
{
    /// <summary>
    /// 所有的Id都是int类型来实现
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EfRepository<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>
         where TDbContext : DbContext
         where TEntity : Entity
    {
        public EfRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}