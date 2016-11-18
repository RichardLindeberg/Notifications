namespace Notifications.Messages.Commands
{
    using System;

    public class RemoveFireBaseTokenCommand : IPersonCommand
    {
        public RemoveFireBaseTokenCommand(string personalNumber, string firebaseToken, Guid commandId)
        {
            PersonalNumber = personalNumber;
            FirebaseToken = firebaseToken;
            CommandId = commandId;
        }

        public Guid CommandId { get; set; }

        public string PersonalNumber { get; set; }

        public string FirebaseToken { get; set; }
    }
}