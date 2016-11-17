namespace Notifications.Messages
{
    public class SentFirebaseMessage : PersonEvent
    {
        public SentFirebaseMessage(string personalNumber, string messageId, string message)
            : base(personalNumber)
        {
            MessageId = messageId;
            Message = message;
        }

        public string MessageId { get; }

        public string Message { get; }
    }
}