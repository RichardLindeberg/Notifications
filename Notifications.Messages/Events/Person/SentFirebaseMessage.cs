namespace Notifications.Messages.Events.Person
{
    public class SentFirebaseMessage : PersonEvent
    {
        public SentFirebaseMessage(string personalNumber, string messageId, string message, string notificationTypeId)
            : base(personalNumber)
        {
            MessageId = messageId;
            Message = message;
            NotificationTypeId = notificationTypeId;
        }

        public string MessageId { get; }

        public string Message { get; }

        public string NotificationTypeId { get; }
    }
}