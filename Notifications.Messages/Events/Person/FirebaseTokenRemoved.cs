namespace Notifications.Messages.Events.Person
{
    public class FirebaseTokenRemoved : PersonEvent, IFirebaseTokenRemoved
    {
        public FirebaseTokenRemoved(string personalNumber, string firebaseToken, string notificationTypeId)
            : base(personalNumber)
        {
            FirebaseToken = firebaseToken;
            NotificationTypeId = notificationTypeId;
        }

        public string FirebaseToken { get; }

        public string NotificationTypeId { get; }
    }
}