namespace Notifications.Domain
{
    public class FireBaseNotificationResponse
    {
        public FireBaseNotificationResponse(bool wasSent, bool shouldBeDeleted, string errorMessage, string firebaseMessageId)
        {
            WasSent = wasSent;
            ShouldBeDeleted = shouldBeDeleted;
            ErrorMessage = errorMessage;
            FirebaseMessageId = firebaseMessageId;
        }

        public string FirebaseMessageId { get; }

        public bool WasSent { get; }

        public bool ShouldBeDeleted { get; }

        public string ErrorMessage { get; }
    }
}