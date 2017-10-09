using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Entity;
using Dark.Core.Domain.Uow;
using Dark.Core.Events;
using Dark.Core.Events.Bus;
using Dark.Core.Events.Bus.Entities;
using Dark.Core.Extension;
using Dark.Core.Runtime.Session;
using Dark.Core.Utils;
using EntityFramework.DynamicFilters;

namespace Dark.EntityFramework
{
    public abstract class BaseDbContext<TUser, TRole> : DbContext, ITransientDependency
        where TUser : class, IEntity<int>
        where TRole : class, IEntity<int>
    {
        #region 0.0 公共属性
        public ILogger Logger { get; set; }

        public IBaseSession Session { get; set; }

        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

        public IEventBus EventBus { get; set; }

        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }

        #endregion

        #region 1.0 构造函数
        /// <summary>
        /// Constructor.
        /// Uses <see cref="IAbpStartupConfiguration.DefaultNameOrConnectionString"/> as connection string.
        /// </summary>
        protected BaseDbContext()
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseDbContext(DbCompiledModel model)
            : base(model)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            InitializeDbContext();
        }

        #endregion

        #region 2.0 提交事务
        public override int SaveChanges()
        {
            try
            {
                var changedEntities = ApplyAbpConcepts();
                var result = base.SaveChanges();
                EntityChangeEventHelper.TriggerEvents(changedEntities);
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var changeReport = ApplyAbpConcepts();
                var result = await base.SaveChangesAsync(cancellationToken);
                await EntityChangeEventHelper.TriggerEventsAsync(changeReport);
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        #endregion

        #region 3.0 系统自带的默认表

        public virtual IDbSet<Sys_Role> Sys_Roles { get; set; }
        public virtual IDbSet<Sys_UserRole> Sys_UserRoles { get; set; }
        public virtual IDbSet<Sys_UserClaim> Sys_UserClaims { get; set; }
        public virtual IDbSet<Sys_UserLogin> Sys_UserLogins { get; set; }
        public virtual IDbSet<Sys_Permission> Sys_Permissions { get; set; }
        public virtual IDbSet<Sys_Account> Sys_Accounts { get; set; }
        #endregion


        #region 4.0 私有方法
        /// <summary>
        /// 4.1  dbcontext 初始化方法
        /// </summary>
        private void InitializeDbContext()
        {
            SetNullsForInjectedProperties();
            RegisterToChanges();
        }

        /// <summary>
        /// 4.2 初始化属性
        /// </summary>
        private void SetNullsForInjectedProperties()
        {
            Logger = NullLogger.Instance;
            Session = NullSession.Instance;
            EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            //GuidGenerator = SequentialGuidGenerator.Instance;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// 注册实体变化的事件
        /// </summary>
        private void RegisterToChanges()
        {
            ((IObjectContextAdapter)this)
                .ObjectContext
                .ObjectStateManager
                .ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;
        }



        public virtual void Initialize()
        {
            Database.Initialize(false);
            //this.SetFilterScopedParameterValue(AbpDataFilters.MustHaveTenant, AbpDataFilters.Parameters.TenantId,
            //    AbpSession.TenantId ?? 0);
            //this.SetFilterScopedParameterValue(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId,
            //    AbpSession.TenantId);
        }

        #endregion

        #region 5.0 事件

        protected virtual void ObjectStateManager_ObjectStateManagerChanged(object sender,
            System.ComponentModel.CollectionChangeEventArgs e)
        {
            var contextAdapter = (IObjectContextAdapter)this;
            if (e.Action != CollectionChangeAction.Add)
            {
                return;
            }

            var entry = contextAdapter.ObjectContext.ObjectStateManager.GetObjectStateEntry(e.Element);
            switch (entry.State)
            {
                case EntityState.Added:
                    CheckAndSetId(entry.Entity);
                    EntityAuditingHelper.SetCreationAuditProperties(entry, GetAuditUserId());
                    break;
                    //case EntityState.Deleted: //It's not going here at all
                    //    SetDeletionAuditProperties(entry.Entity, GetAuditUserId());
                    //    break;
            }
        }

        #endregion



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //过滤状态作废的数据
            modelBuilder.Filter("SoftDelete", (IHasDelete d) => d.IsDel, false);
        }


        protected virtual EntityChangeReport ApplyAbpConcepts()
        {
            var changeReport = new EntityChangeReport();

            var userId = GetAuditUserId();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                ApplyAbpConcepts(entry, userId, changeReport);
            }

            return changeReport;
        }

        protected virtual void ApplyAbpConcepts(DbEntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyAbpConceptsForAddedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Modified:
                    ApplyAbpConceptsForModifiedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Deleted:
                    ApplyAbpConceptsForDeletedEntity(entry, userId, changeReport);
                    break;
            }

            AddDomainEvents(changeReport.DomainEvents, entry.Entity);
        }

        protected virtual void ApplyAbpConceptsForAddedEntity(DbEntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            CheckAndSetId(entry.Entity);
            EntityAuditingHelper.SetCreationAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Created));
        }

        protected virtual void ApplyAbpConceptsForModifiedEntity(DbEntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            EntityAuditingHelper.SetModificationAuditProperties(entry.Entity, userId);
            if (entry.Entity is IHasDelete && entry.Entity.As<IHasDelete>().IsDel)
            {
                EntityAuditingHelper.SetDeletionAuditProperties(entry.Entity, userId);
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
            }
            else
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Updated));
            }
        }

        protected virtual void ApplyAbpConceptsForDeletedEntity(DbEntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            //设置实体的状态
            entry.State = EntityState.Modified;
            EntityAuditingHelper.SetDeletionAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
        }

        protected virtual void AddDomainEvents(List<DomainEventEntry> domainEvents, object entityAsObj)
        {
            var generatesDomainEventsEntity = entityAsObj as IGeneratesDomainEvents;
            if (generatesDomainEventsEntity == null)
            {
                return;
            }

            if (generatesDomainEventsEntity.DomainEvents.IsNullOrEmpty())
            {
                return;
            }

            domainEvents.AddRange(
                generatesDomainEventsEntity.DomainEvents.Select(
                    eventData => new DomainEventEntry(entityAsObj, eventData)));
            generatesDomainEventsEntity.DomainEvents.Clear();
        }

        protected virtual void CheckAndSetId(object entityAsObj)
        {
            //Set GUID Ids
            var entity = entityAsObj as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                var entityType = ObjectContext.GetObjectType(entityAsObj.GetType());
                var idProperty = entityType.GetProperty("Id");
                var dbGeneratedAttr =
                    ReflectionTools.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
                if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                {
                    entity.Id = Guid.NewGuid();
                }
            }
        }

        protected virtual void LogDbEntityValidationException(DbEntityValidationException exception)
        {
            Logger.Error("There are some validation errors while saving changes in EntityFramework:");
            foreach (var ve in exception.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
            {
                Logger.Error(" - " + ve.PropertyName + ": " + ve.ErrorMessage);
            }
        }

        protected virtual long? GetAuditUserId()
        {
            if (Session.UserId.HasValue &&
                CurrentUnitOfWorkProvider != null)
            {
                return Session.UserId;
            }
            return null;
        }


    }
}
