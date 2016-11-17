using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Messages
{
    public class Event
    {
        public Event()
        {
            EventId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid EventId { get; }

        public DateTime CreatedAt { get; }
    }
}
