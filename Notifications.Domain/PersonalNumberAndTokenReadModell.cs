using Notifications.Messages.Events;
using Notifications.Messages.Events.Person;

namespace Notifications.Domain
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Messages;

    public class PersonalNumberAndTokenReadModell
    {
        private readonly object _lockObject = new object();
        public PersonalNumberAndTokenReadModell()
        {
            PeopleWithTokens = new List<PersonWithToken>();
        }

        public List<PersonWithToken> PeopleWithTokens { get; }

        public bool TokenExistsOnOtherPersonalNumber(string personalNumber, string firebaseToken)
        {
            lock (_lockObject)
            {
                var people = PeopleWithTokens.Where(t => t.Token == firebaseToken);
                return people.Any(t => t.PersonalNumber != personalNumber);
            }
        }

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
                lock (_lockObject)
                {
                    var pwt = new PersonWithToken(tokenAdded.PersonalNumber, tokenAdded.FirebaseToken);
                    if (PeopleWithTokens.Contains(pwt) == false)
                    {
                        PeopleWithTokens.Add(pwt);
                    }
                }
            }
        }

        private void TokenRemove(Event evt)
        {
            var firebaseTokenRemoved = evt as FirebaseTokenRemoved;
            if (firebaseTokenRemoved != null)
            {
                lock (_lockObject)
                {
                    var pwt = new PersonWithToken(
                        firebaseTokenRemoved.PersonalNumber,
                        firebaseTokenRemoved.FirebaseToken);
                    PeopleWithTokens.Remove(pwt);
                }
            }
        }
    }
}