namespace Notifications.Messages.Commands
{
    using System;

    public class SendMessageCommand : IPersonCommand
    {
        public SendMessageCommand(string personalNumber, string messageId, string message, Guid commandId)
        {
            PersonalNumber = personalNumber;
            MessageId = messageId;
            Message = message;
            CommandId = commandId;
        }

        public Guid CommandId { get; set; }

        public string PersonalNumber { get; set; }

        public string MessageId { get; set; }

        public string Message { get; set; }
    }
}