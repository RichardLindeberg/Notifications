namespace Notifications.Storage.ReadModells
{
    using Notifications.Messages.Events;

    public interface IReadModellWriter
    {
        string GetLastCommit();

        void NewEvent(Event @event, string commitId);
    }
}