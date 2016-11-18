namespace Notifications.Domain
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using Messages;

    public class PeopleReadModelsHandler
    {
        public PeopleReadModelsHandler()
        {
            PeopleWithTokens = new ConcurrentDictionary<PersonWithToken, PersonWithToken>();
        }

        public ConcurrentDictionary<PersonWithToken, PersonWithToken> PeopleWithTokens { get; }

        public void Handle(Event evt)
        {
            TokenAdd(evt);
            TokenRemove(evt);
        }

        private void TokenAdd(Event evt)
        {
            var tokenAdded = evt as FirebaseTokenAdded;
            if (tokenAdded != null)
            {
                var pwt = new PersonWithToken(tokenAdded.PersonalNumber, tokenAdded.FirebaseToken);
                PeopleWithTokens.GetOrAdd(pwt, pwt);
            }
        }

        private void TokenRemove(Event evt)
        {
            var firebaseTokenRemoved = evt as FirebaseTokenRemoved;
            if (firebaseTokenRemoved != null)
            {
                var pwt = new PersonWithToken(firebaseTokenRemoved.PersonalNumber, firebaseTokenRemoved.FirebaseToken);
                PersonWithToken returnoutValue;
                PeopleWithTokens.TryRemove(pwt, out returnoutValue);
            }
        }
    }
}