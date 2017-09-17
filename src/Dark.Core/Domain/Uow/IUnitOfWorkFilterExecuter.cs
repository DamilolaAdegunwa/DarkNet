using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Uow
{
    #region 1.0 接口 IUnitOfWorkFilterExecuter
    public interface IUnitOfWorkFilterExecuter
    {
        void ApplyDisableFilter(IUnitOfWork unitOfWork, string filterName);
        void ApplyEnableFilter(IUnitOfWork unitOfWork, string filterName);
        void ApplyFilterParameterValue(IUnitOfWork unitOfWork, string filterName, string parameterName, object value);
    }
    #endregion

    #region 2.0 空实现 NullUnitOfWorkFilterExecuter
    public class NullUnitOfWorkFilterExecuter : IUnitOfWorkFilterExecuter
    {
        public void ApplyDisableFilter(IUnitOfWork unitOfWork, string filterName)
        {

        }

        public void ApplyEnableFilter(IUnitOfWork unitOfWork, string filterName)
        {

        }

        public void ApplyFilterParameterValue(IUnitOfWork unitOfWork, string filterName, string parameterName, object value)
        {

        }
    } 
    #endregion
}
