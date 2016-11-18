namespace Notifications.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using Messages;

    public class Person
    {
        private readonly IFirebaseNotificationSender _firebaseNotificationSender;

        private readonly List<string> _SentMessageIds;

        private readonly List<PersonEvent> _newEvents;

        public Person(string personalNumber, IFirebaseNotificationSender firebaseNotificationSender)
        {
            _firebaseNotificationSender = firebaseNotificationSender;
            _newEvents = new List<PersonEvent>();
            _SentMessageIds = new List<string>();
            PersonalNumber = personalNumber;
            FirebaseTokens = new List<string>();
        }

        public IEnumerable<PersonEvent> NewEvents => _newEvents;

        public string PersonalNumber { get; }

        public List<string> FirebaseTokens { get; }

        public void Apply(IEnumerable<PersonEvent> events)
        {
            foreach (var personEvent in events)
            {
                CheckPersonalNumber(personEvent);
                Apply((dynamic)personEvent);
            }
        }

        public void Apply(FirebaseTokenAdded evt)
        {
            if (FirebaseTokens.Contains(evt.FirebaseToken) == false)
            {
                FirebaseTokens.Add(evt.FirebaseToken);
            }
        }

        public void Apply(FailedToSendFirebaseMessage evt)
        {
            
        }

        public void Apply(SentFirebaseMessage evt)
        {
            _SentMessageIds.Add(evt.MessageId);
        }

        public void Apply(FirebaseTokenRemoved evt)
        {
            FirebaseTokens.RemoveAll(t => t == evt.FirebaseToken);
        }

        public void AddToken(string fireBaseToke)
        {
            if (FirebaseTokens.Contains(fireBaseToke) == false)
            {
                var evt = new FirebaseTokenAdded(PersonalNumber, fireBaseToke);
                Publish(evt);
            }
        }

        public void RemoveToken(string fireBaseToken)
        {
            if (FirebaseTokens.Contains(fireBaseToken))
            {
                var evt = new FirebaseTokenRemoved(PersonalNumber, fireBaseToken);
                Publish(evt);
            }
        }

        public void SendMessage(string messageId, string message)
        {
            var tokens = FirebaseTokens.ToList();
            foreach (var firebaseToken in tokens)
            {
                var response = _firebaseNotificationSender.SendNotification(firebaseToken, message);
                if (response.WasSent)
                {
                    var evt = new SentFirebaseMessage(PersonalNumber, messageId, message);
                    Publish(evt);
                }
                else
                {
                    if (response.ShouldBeDeleted)
                    {
                        RemoveToken(firebaseToken);
                    }
                    var evt = new FailedToSendFirebaseMessage(PersonalNumber, message, response.ErrorMessage);
                    Publish(evt);
                }
            }
        }

        private void Publish(PersonEvent evt)
        {
            _newEvents.Add(evt);
            CheckPersonalNumber(evt);
            Apply((dynamic)evt);
        }

        private void CheckPersonalNumber(PersonEvent evt)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            if (PersonalNumber != evt.PersonalNumber)
            {
                throw new BadEventInStreamException("There was an event in the stream belonging to another aggregate");
            }
        }
    }
}