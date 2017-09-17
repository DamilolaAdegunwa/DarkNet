using System;

namespace Dark.Core.Domain.Uow
{
    public class UnitOfWorkFailedEventArgs : EventArgs
    {
        private Exception exception;

        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            this.exception = exception;
        }
    }
}