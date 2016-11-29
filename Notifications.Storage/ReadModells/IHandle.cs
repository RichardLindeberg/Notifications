namespace Notifications.Storage.ReadModells
{
    using System;

    using Notifications.Messages.Events;

    public interface IHandle<in T> where T : Event
    {
        void Handle(T evnt, Guid commitId);
    }
}