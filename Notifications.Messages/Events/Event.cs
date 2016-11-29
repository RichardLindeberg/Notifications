using System;

namespace Notifications.Messages.Events
{
    public class Event : IEvent
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
