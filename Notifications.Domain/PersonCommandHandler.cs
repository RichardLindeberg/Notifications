using Notifications.Domain.ReadModell;

namespace Notifications.Domain
{
    using System;

    using Notifications.Messages.Commands;

    public class PersonCommandHandler
    {
        private readonly IPersonExecutor _personExecutor;

        private readonly IPersonalNumberAndTokenReadModell _personalNumberAndTokenReadModell;

        public PersonCommandHandler(IPersonExecutor personExecutor, IPersonalNumberAndTokenReadModell personalNumberAndTokenReadModell )
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

            if (command.PersonalNumber.Length != 12)
            {
                throw new ArgumentException("Personal number must be 12 characters");
            }

            //if (_personalNumberAndTokenReadModell.TokenExistsOnOtherPersonalNumber(
            //    command.PersonalNumber,
            //    command.FirebaseToken))
            //{
            //    //_personExecutor.Execute("1234", person => person.RemoveToken(), Guid.NewGuid());
            //    throw new InvalidOperationException("Token already used by other person");

            //}
            _personExecutor.Execute(command.PersonalNumber, p => p.AddToken(command.FirebaseToken, command.NotificationTypeId), command.CommandId);
        }

        public void Handle(RemoveFireBaseTokenCommand command)
        {
            _personExecutor.Execute(command.PersonalNumber, person => person.RemoveToken(command.FirebaseToken, command.NotificationTypeId), command.CommandId);
        }

        public void Handle(RemoveFireBaseTokenDueToDuplicateCommand command)
        {
            _personExecutor.Execute(command.PersonalNumber, person => person.RemoveTokenDueToDuplicate(command.FirebaseToken, command.DuplicateWithPersonalNumber), command.CommandId);

        }

        public void Handle(SendMessageCommand command)
        {
            _personExecutor.Execute(command.PersonalNumber, person => person.SendMessage(command.MessageId, command.Message, command.NotificationTypeId), command.CommandId);
        }
    }
}