﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Uow;
using Dark.Core.Extension;
using Dark.Core.Reflections;
using EntityFramework.DynamicFilters;

namespace Dark.EntityFramework.Uow
{

    #region 1.0 接口 IEfUnitOfWorkFilterExecuter
    public interface IEfUnitOfWorkFilterExecuter : IUnitOfWorkFilterExecuter
    {
        void ApplyCurrentFilters(IUnitOfWork unitOfWork, DbContext dbContext);
    }

    #endregion

    #region 2.0 具体实现 EfDynamicFiltersUnitOfWorkFilterExecuter
    public class EfDynamicFiltersUnitOfWorkFilterExecuter : IEfUnitOfWorkFilterExecuter
    {
        public void ApplyDisableFilter(IUnitOfWork unitOfWork, string filterName)
        {
            foreach (var activeDbContext in unitOfWork.As<EfUnitOfWork>().GetAllActiveDbContexts())
            {
                activeDbContext.DisableFilter(filterName);
            }
        }

        public void ApplyEnableFilter(IUnitOfWork unitOfWork, string filterName)
        {
            foreach (var activeDbContext in unitOfWork.As<EfUnitOfWork>().GetAllActiveDbContexts())
            {
                activeDbContext.EnableFilter(filterName);
            }
        }

        public void ApplyFilterParameterValue(IUnitOfWork unitOfWork, string filterName, string parameterName, object value)
        {
            foreach (var activeDbContext in unitOfWork.As<EfUnitOfWork>().GetAllActiveDbContexts())
            {
                if (TypeHelper.IsFunc<object>(value))
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, (Func<object>)value);
                }
                else
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, value);
                }
            }
        }

        public void ApplyCurrentFilters(IUnitOfWork unitOfWork, DbContext dbContext)
        {
            foreach (var filter in unitOfWork.Filters)
            {
                if (filter.IsEnabled)
                {
                    dbContext.EnableFilter(filter.FilterName);
                }
                else
                {
                    dbContext.DisableFilter(filter.FilterName);
                }

                foreach (var filterParameter in filter.FilterParameters)
                {
                    if (TypeHelper.IsFunc<object>(filterParameter.Value))
                    {
                        dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, (Func<object>)filterParameter.Value);
                    }
                    else
                    {
                        dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, filterParameter.Value);
                    }
                }
            }
        }
    }
    #endregion
}
