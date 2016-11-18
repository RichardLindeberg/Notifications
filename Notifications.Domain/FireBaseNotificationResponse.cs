namespace Notifications.Domain
{
    public class FireBaseNotificationResponse
    {
        public FireBaseNotificationResponse(bool wasSent, bool shouldBeDeleted, string errorMessage)
        {
            WasSent = wasSent;
            ShouldBeDeleted = shouldBeDeleted;
            ErrorMessage = errorMessage;
        }

        public bool WasSent { get; }

        public bool ShouldBeDeleted { get; }

        public string ErrorMessage { get; }
    }
}