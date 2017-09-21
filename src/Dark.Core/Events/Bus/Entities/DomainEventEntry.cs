using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Events.Bus.Entities
{
    [Serializable]
    public class DomainEventEntry
    {
        public object SourceEntity { get; }

        public IEventData EventData { get; }

        public DomainEventEntry(object sourceEntity, IEventData eventData)
        {
            SourceEntity = sourceEntity;
            EventData = eventData;
        }
    }
}
