namespace Notifications.Domain
{
    using System;

    using Notifications.Messages.Commands;

    public class PersonCommandHandler
    {
        private readonly PersonExecutor _personExecutor;

        private readonly PersonalNumberAndTokenReadModell _personalNumberAndTokenReadModell;

        public PersonCommandHandler(PersonExecutor personExecutor, PersonalNumberAndTokenReadModell personalNumberAndTokenReadModell )
        {
            _personExecutor = personExecutor;
            _personalNumberAndTokenReadModell = personalNumberAndTokenReadModell;
        }

        public void Handle(AddFireBaseTokenCommand command)
        {
            if (_personalNumberAndTokenReadModell.TokenExistsOnOtherPersonalNumber(
                command.PersonalNumber,
                command.FirebaseToken))
            {
                throw new InvalidOperationException("Token already used by other person");
            }
            _personExecutor.Execute(command.PersonalNumber, p => p.AddToken(command.FirebaseToken), command.CommandId);
        }

        public void Handle(RemoveFireBaseTokenCommand command)
        {
            _personExecutor.Execute(command.PersonalNumber, person => person.RemoveToken(command.FirebaseToken), command.CommandId);
        }

        public void Handle(SendMessageCommand command)
        {
            _personExecutor.Execute(command.PersonalNumber, person => person.SendMessage(command.MessageId, command.Message), command.CommandId);
        }
    }
}