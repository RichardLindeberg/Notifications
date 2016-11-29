namespace Notifications.Messages.Events.Person
{
    public class FirebaseTokenRemovedDueToDuplicate : PersonEvent, IFirebaseTokenRemoved
    {
        public FirebaseTokenRemovedDueToDuplicate(string personalNumber, string duplicateWithPersonalNumber, string firebaseToken, string notificationTypeId)
            : base(personalNumber)
        {
            DuplicateWithPersonalNumber = duplicateWithPersonalNumber;
            FirebaseToken = firebaseToken;
            NotificationTypeId = notificationTypeId;
        }

        public string DuplicateWithPersonalNumber { get; }

        public string FirebaseToken { get; }

        public string NotificationTypeId { get; }
    }
}