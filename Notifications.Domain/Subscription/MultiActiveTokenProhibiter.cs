namespace Notifications.Domain.Subscription
{
    using System;
    using System.Linq;

    using Notifications.Domain.ReadModell;
    using Notifications.Messages.Commands;
    using Notifications.Messages.Events;
    using Notifications.Messages.Events.Person;

    public class MultiActiveTokenProhibiter : ISubscriptionConsumer, IHandle<FirebaseTokenAdded>
    {
        private readonly ISubscriptionConsumerCommiter _commiter;

        private readonly IPersonalNumberAndTokenReadModell _tokenReadModell;

        private readonly PersonCommandHandler _commandHandler;

        public MultiActiveTokenProhibiter(ISubscriptionConsumerCommiter commiter, IPersonalNumberAndTokenReadModell tokenReadModell, PersonCommandHandler commandHandler)
        {
            _commiter = commiter;
            _tokenReadModell = tokenReadModell;
            _commandHandler = commandHandler;
        }

        public string GetCheckPointToken()
        {
            return _commiter.GetCheckPointToken();
        }

        public void NewEvent(Event @event, string checkPointToken)
        {
            var tokenAdded = @event as FirebaseTokenAdded;
            if (tokenAdded != null)
            {
                Handle(tokenAdded, checkPointToken);
            } 
            _commiter.StoreCheckPointToken(checkPointToken);
        }

        public void Handle(FirebaseTokenAdded @event, string checkPointToken)
        {
            var allUsingThis = _tokenReadModell.GetTokenUsage(@event.FirebaseToken);
            var otherUsingThisToken = allUsingThis.Where(t => t.PersonalNumber != @event.PersonalNumber);
            foreach (var personWithToken in otherUsingThisToken)
            {
                var log = NLog.LogManager.GetCurrentClassLogger();
                log.Info($"Will disable token: {personWithToken.Token} for person {personWithToken.PersonalNumber} since the token was added by {@event.PersonalNumber}");
                try
                {
                    _commandHandler.Handle(new RemoveFireBaseTokenDueToDuplicateCommand(@event.PersonalNumber, personWithToken.PersonalNumber,  personWithToken.Token, Guid.NewGuid()));

                    log.Info($"Disable multiple token excuted for {personWithToken.PersonalNumber}");
                }
                catch (Exception ex)
                {
                    log.Error(ex, $"Failed to remove token from {personWithToken.PersonalNumber}");
                    throw;
                }
            }
        }
    }
}