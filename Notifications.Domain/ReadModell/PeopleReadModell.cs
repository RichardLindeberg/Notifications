using System.Collections.Concurrent;
using Notifications.Messages.Events;
using Notifications.Messages.Events.Person;

namespace Notifications.Domain.ReadModell
{
    public class PeopleReadModell : IReadModell
    {
        public PeopleReadModell()
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
                var pwt = new PersonWithToken(
                    tokenAdded.PersonalNumber,
                    tokenAdded.FirebaseToken,
                    tokenAdded.NotificationTypeId);
                {
                    PeopleWithTokens.TryAdd(pwt, pwt);
                }
            }
        }

        private void TokenRemove(Event evt)
        {
            var firebaseTokenRemoved = evt as FirebaseTokenRemoved;
            if (firebaseTokenRemoved != null)
            {
                PersonWithToken outPwt;
                var pwt = new PersonWithToken(
                    firebaseTokenRemoved.PersonalNumber,
                    firebaseTokenRemoved.FirebaseToken,
                    firebaseTokenRemoved.NotificationTypeId);
                PeopleWithTokens.TryRemove(pwt, out outPwt);
            }
        }
    }
}