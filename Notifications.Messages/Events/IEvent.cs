namespace Notifications.Messages.Events
{
    using System;

    public interface IEvent
    {
        Guid EventId { get; }

        DateTime CreatedAt { get; }
    }
}