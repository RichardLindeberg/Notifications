namespace Notifications.Domain
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Messages.Events;
    using Messages.Events.Person;

    public class PersonalNumberAndTokenReadModell
    {
        private readonly object _lockObject = new object();

        public PersonalNumberAndTokenReadModell()
        {
            PeopleWithTokens = new ConcurrentDictionary<PersonWithToken, PersonWithToken>();
            FirebaseTokensAndListOfPople = new ConcurrentDictionary<string, List<string>>();
        }

        public ConcurrentDictionary<PersonWithToken, PersonWithToken> PeopleWithTokens { get; }

        public ConcurrentDictionary<string, List<string>> FirebaseTokensAndListOfPople { get; }

        public bool TokenExistsOnOtherPersonalNumber(string personalNumber, string firebaseToken)
        {
            //lock (_lockObject)
            //{
            //    var people = PeopleWithTokens.Where(t => t.Value.Token == firebaseToken);
            //    return people.Any(t => t.Value.PersonalNumber != personalNumber);
            //}
            List<string> pnos;
            if (FirebaseTokensAndListOfPople.TryGetValue(firebaseToken, out pnos))
            {
                return pnos.Any(t => t == personalNumber);
            }
            return false;
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
                //lock (_lockObject)
                {
                    var pwt = new PersonWithToken(tokenAdded.PersonalNumber, tokenAdded.FirebaseToken, tokenAdded.NotificationTypeId);
                    //if (PeopleWithTokens.ContainsKey(pwt) == false)
                    {
                        PeopleWithTokens.TryAdd(pwt, pwt);
                    }
                }
                List<string> pnos;
                if (FirebaseTokensAndListOfPople.TryGetValue(tokenAdded.FirebaseToken, out pnos))
                {
                    if (pnos.Contains(tokenAdded.PersonalNumber) == false)
                    {
                        var newPnos = pnos.ToList();
                        newPnos.Add(tokenAdded.PersonalNumber);
                        if (FirebaseTokensAndListOfPople.TryUpdate(tokenAdded.FirebaseToken, newPnos, pnos) == false)
                        {
                            throw new InvalidOperationException("Kabom - concurrency exception");
                        }
                    }
                }
            }
        }

        private void TokenRemove(Event evt)
        {
            var firebaseTokenRemoved = evt as FirebaseTokenRemoved;
            if (firebaseTokenRemoved != null)
            {
                //lock (_lockObject)
                {
                    PersonWithToken outPwt;
                    var pwt = new PersonWithToken(
                        firebaseTokenRemoved.PersonalNumber,
                        firebaseTokenRemoved.FirebaseToken, 
                        firebaseTokenRemoved.NotificationTypeId);
                    PeopleWithTokens.TryRemove(pwt, out outPwt);
                }
                List<string> pnos;
                if (FirebaseTokensAndListOfPople.TryGetValue(firebaseTokenRemoved.FirebaseToken, out pnos))
                {
                    if (pnos.Contains(firebaseTokenRemoved.PersonalNumber) == false)
                    {
                        var newPnos = pnos.ToList();
                        newPnos.Remove(firebaseTokenRemoved.PersonalNumber);
                        if (FirebaseTokensAndListOfPople.TryUpdate(firebaseTokenRemoved.FirebaseToken, newPnos, pnos) == false)
                        {
                            throw new InvalidOperationException("Kabom - concurrency exception");
                        }
                    }
                }
            }
        }
    }
}