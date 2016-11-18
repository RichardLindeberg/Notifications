namespace Notifications.Domain
{
    public interface IFirebaseNotificationSender
    {
        FireBaseNotificationResponse SendNotification(string firebaseToken, string message);
    }
}