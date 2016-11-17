namespace Notifications.Messages
{
    public class FailedToSendFirebaseMessage : PersonEvent
    {
        public FailedToSendFirebaseMessage(string personalNumber, string message, string errorMessage)
            : base(personalNumber)
        {
            Message = message;
            ErrorMessage = errorMessage;
        }

        public string Message { get; }

        public string ErrorMessage { get; }
    }
}