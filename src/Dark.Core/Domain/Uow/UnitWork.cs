using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Dark.Core.Domain.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope trans = null;
        public UnitOfWork()
        {
            trans = new TransactionScope();
        }


        public void Commit()
        {
            trans?.Complete();//必须要调用scope.Complete()才能将数据更新到数据库
        }

        public void Dispose()
        {
            trans?.Dispose();
        }
    }
}
