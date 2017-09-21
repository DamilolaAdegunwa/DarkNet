using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Events
{


    /// <summary>
    /// Defines interface for all Event data classes.
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// The time when the event occured.
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        object EventSource { get; set; }
    }

    /// <summary>
    /// Implements <see cref="IEventData"/> and provides a base for event data classes.
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// The time when the event occurred.
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected EventData()
        {
            EventTime = DateTime.Now;
        }
    }
}
