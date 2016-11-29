namespace Notifications.Domain.Subscription
{
    public interface ISubscriptionConsumerCommiter
    {
        string GetCheckPointToken();

        void StoreCheckPointToken(string checkPointToken);
    }
}