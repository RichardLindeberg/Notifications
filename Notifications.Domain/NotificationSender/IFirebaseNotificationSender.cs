namespace Notifications.Domain.NotificationSender
{
    public interface IFirebaseNotificationSender
    {
        FireBaseNotificationResponse SendNotification(string firebaseToken, string message);
    }
}
    
