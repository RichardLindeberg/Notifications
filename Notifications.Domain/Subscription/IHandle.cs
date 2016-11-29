namespace Notifications.Domain.Subscription
{
    using System;

    using Notifications.Messages.Events;

    public interface IHandle<in T> where T : IEvent
    {
        void Handle(T evnt, string checkPointToken);
    }
}