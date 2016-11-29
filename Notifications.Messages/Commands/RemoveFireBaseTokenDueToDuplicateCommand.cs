namespace Notifications.Messages.Commands
{
    using System;

    public class RemoveFireBaseTokenDueToDuplicateCommand : IPersonCommand
    {
        public RemoveFireBaseTokenDueToDuplicateCommand(string duplicateWithPersonalNumber, string personalNumber, string firebaseToken, Guid commandId)
        {
            DuplicateWithPersonalNumber = duplicateWithPersonalNumber;
            CommandId = commandId;
            PersonalNumber = personalNumber;
            FirebaseToken = firebaseToken;
        }

        public string DuplicateWithPersonalNumber { get; }


        public Guid CommandId { get; set; }

        public string PersonalNumber { get; set; }

        public string FirebaseToken { get; set; }
    }
}