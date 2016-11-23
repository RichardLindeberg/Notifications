namespace Notifications.Domain
{
    using System;

    using Notifications.Messages.Commands;

    public class PersonCommandHandler
    {
        private readonly IPersonExecutor _personExecutor;

        private readonly PersonalNumberAndTokenReadModell _personalNumberAndTokenReadModell;

        public PersonCommandHandler(IPersonExecutor personExecutor, PersonalNumberAndTokenReadModell personalNumberAndTokenReadModell )
        {
            _personExecutor = personExecutor;
            _personalNumberAndTokenReadModell = personalNumberAndTokenReadModell;
        }

        public void Handle(AddFireBaseTokenCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (string.IsNullOrEmpty(command.PersonalNumber) || string.IsNullOrEmpty(command.FirebaseToken)
                || string.IsNullOrEmpty(command.NotificationTypeId))
            {
                throw new ArgumentException("Command is not valid, missing requeried parameters");
            }

            if (_personalNumberAndTokenReadModell.TokenExistsOnOtherPersonalNumber(
                command.PersonalNumber,
                command.FirebaseToken))
            {
                throw new InvalidOperationException("Token already used by other person");
            }
            _personExecutor.Execute(command.PersonalNumber, p => p.AddToken(command.FirebaseToken, command.NotificationTypeId), command.CommandId);
        }

        public void Handle(RemoveFireBaseTokenCommand command)
        {
            _personExecutor.Execute(command.PersonalNumber, person => person.RemoveToken(command.FirebaseToken, command.NotificationTypeId), command.CommandId);
        }

        public void Handle(SendMessageCommand command)
        {
            _personExecutor.Execute(command.PersonalNumber, person => person.SendMessage(command.MessageId, command.Message, command.NotificationTypeId), command.CommandId);
        }
    }
}