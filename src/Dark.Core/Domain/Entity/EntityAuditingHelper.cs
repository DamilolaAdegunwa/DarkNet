using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Entity
{
    public static class EntityAuditingHelper
    {
        public static void SetCreationAuditProperties(
            object entityAsObj,
            long? userId)
        {
            var createEntity = entityAsObj as EntityBase;
            if (createEntity == null)
            {
                return;
            }
            if (!userId.HasValue)
            {
                //Unknown user
                return;
            }

            createEntity.CreateTime = DateTime.Now;
            createEntity.Creator = (int)userId;
        }

        public static void SetModificationAuditProperties(
            object entityAsObj,
            long? userId)
        {
            var modifyEntity = entityAsObj as IHasModify;
            if (modifyEntity == null)
            {
                return;
            }
            modifyEntity.LastTime = DateTime.Now;
            modifyEntity.Modifier = userId;
        }


        public static void SetDeletionAuditProperties(
            object entityAsObj,
            long? userId)
        {
            var deleteEntity = entityAsObj as IHasDelete;
            if (deleteEntity == null)
            {
                return;
            }
            
            deleteEntity.DeleteTime = DateTime.Now;
            deleteEntity.DeleteUser = userId;
            deleteEntity.IsDel = true;
        }
    }
}
