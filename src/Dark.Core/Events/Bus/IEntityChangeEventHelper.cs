using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Events.Bus.Entities;

namespace Dark.Core.Events.Bus
{
    /// <summary>
    /// Used to trigger entity change events.
    /// </summary>
    public interface IEntityChangeEventHelper
    {
        void TriggerEvents(EntityChangeReport changeReport);

        Task TriggerEventsAsync(EntityChangeReport changeReport);

        void TriggerEntityCreatingEvent(object entity);

        void TriggerEntityCreatedEventOnUowCompleted(object entity);

        void TriggerEntityUpdatingEvent(object entity);

        void TriggerEntityUpdatedEventOnUowCompleted(object entity);

        void TriggerEntityDeletingEvent(object entity);

        void TriggerEntityDeletedEventOnUowCompleted(object entity);
    }


    /// <summary>
    /// Null-object implementation of <see cref="IEntityChangeEventHelper"/>.
    /// </summary>
    public class NullEntityChangeEventHelper : IEntityChangeEventHelper
    {
        /// <summary>
        /// Gets single instance of <see cref="NullEventBus"/> class.
        /// </summary>
        public static NullEntityChangeEventHelper Instance { get; } = new NullEntityChangeEventHelper();

        private NullEntityChangeEventHelper()
        {

        }

        public void TriggerEntityCreatingEvent(object entity)
        {

        }

        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {

        }

        public void TriggerEntityUpdatingEvent(object entity)
        {

        }

        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {

        }

        public void TriggerEntityDeletingEvent(object entity)
        {

        }

        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {

        }

        public void TriggerEvents(EntityChangeReport changeReport)
        {

        }

        public Task TriggerEventsAsync(EntityChangeReport changeReport)
        {
            return Task.FromResult(0);
        }
    }
}
