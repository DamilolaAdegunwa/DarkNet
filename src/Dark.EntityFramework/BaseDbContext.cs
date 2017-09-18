using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Dark.Core.DI;
using Dark.Core.Domain.Event;
using Dark.Core.Domain.Uow;
using Dark.Core.Runtime.Session;
using Dark.Web.Domain.Entity;

namespace Dark.EntityFramework
{
    public abstract class BaseDbContext : DbContext,ITransientDependency
    {

        public ILogger Logger { get; set; }

        public IBaseSession Session { get; set; }

        public IEventBus EventBus { get; set; }

        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }



        public BaseDbContext() : base()
        {

        }

        public BaseDbContext(string strCon) : base(strCon)
        {

        }


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        #region 1.0 系统自带的默认表
        public virtual IDbSet<Sys_Account> Sys_Accounts { get; set; }
        public virtual IDbSet<Sys_Role> Sys_Roles { get; set; }
        public virtual IDbSet<Sys_UserRole> Sys_UserRoles { get; set; }
        public virtual IDbSet<Sys_UserClaim> Sys_UserClaims { get; set; }
        public virtual IDbSet<Sys_UserLoginAttempts> Sys_UserLoginAttemptss { get; set; }
        public virtual IDbSet<Sys_Permission> Sys_Permissions { get; set; } 
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        private void InitDbContext()
        {
            Logger = NullLogger.Instance;
            Session = NullSession.Instance;
            //EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            //GuidGenerator = SequentialGuidGenerator.Instance;
            EventBus = NullEventBus.Instance;
        }
    }
}
