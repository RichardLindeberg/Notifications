using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Messages.Events.Batch
{
    public abstract class BatchEvent : Event
    {
        protected BatchEvent(string batchId)
        {
            BatchId = batchId;
        }

        public string BatchId { get; }
    }
}
