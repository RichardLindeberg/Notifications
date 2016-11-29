namespace Notifications.Domain.Subscription
{
    using Notifications.Messages.Events;

    public interface ISubscriptionConsumer
    {
        string GetCheckPointToken();

        void NewEvent(Event @event, string checkPointToken);
    }
}