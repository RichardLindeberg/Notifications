using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Notifications.Messages.Events;
using Notifications.Messages.Events.Person;

namespace Notifications.Domain.ReadModell
{
    public interface IReadModell
    {
        void Handle(Event evt);
    }

    public class PersonalNumberAndTokenReadModell : IReadModell
    {
        public PersonalNumberAndTokenReadModell()
        {
            FirebaseTokensAndListOfPople = new ConcurrentDictionary<string, List<string>>();
        }

        private ConcurrentDictionary<string, List<string>> FirebaseTokensAndListOfPople { get; }

        public List<string> PersonalNumberWithThisToken(string firebaseToken)
        {
            List<string> pnos;
            if (FirebaseTokensAndListOfPople.TryGetValue(firebaseToken, out pnos))
            {
                return pnos;
            }
            return new List<string>();
        }

        public bool TokenExistsOnOtherPersonalNumber(string personalNumber, string firebaseToken)
        {
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
                List<string> pnos = new List<string>() { tokenAdded.PersonalNumber };
                
                FirebaseTokensAndListOfPople.AddOrUpdate(
                    tokenAdded.FirebaseToken,
                    pnos,
                    (key, existingValue) =>
                        {
                            if (existingValue.Contains(tokenAdded.PersonalNumber) == false)
                            {
                                existingValue.Add(tokenAdded.PersonalNumber);
                            }
                            return existingValue;
                        });
            }
        }

        private void TokenRemove(Event evt)
        {
            var firebaseTokenRemoved = evt as FirebaseTokenRemoved;
            if (firebaseTokenRemoved != null)
            {
                List<string> pnos;
                if (FirebaseTokensAndListOfPople.TryGetValue(firebaseTokenRemoved.FirebaseToken, out pnos))
                {
                    if (pnos.Contains(firebaseTokenRemoved.PersonalNumber))
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