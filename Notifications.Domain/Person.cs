namespace Notifications.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Messages.Events.Person;

    public class Person
    {
        private readonly IFirebaseNotificationSender _firebaseNotificationSender;

        private readonly List<string> _sentMessageIds;

        private readonly List<PersonEvent> _newEvents;

        private readonly List<FirebaseTokenAndNotificationTypeId> _firebaseTokenAndNotificationTypeIds;

        public Person(string personalNumber, IFirebaseNotificationSender firebaseNotificationSender)
        {
            _firebaseNotificationSender = firebaseNotificationSender;
            _newEvents = new List<PersonEvent>();
            _sentMessageIds = new List<string>();
            PersonalNumber = personalNumber;
            _firebaseTokenAndNotificationTypeIds = new List<FirebaseTokenAndNotificationTypeId>();
        }

        public IEnumerable<PersonEvent> NewEvents => _newEvents;

        public string PersonalNumber { get; }

        public IEnumerable<FirebaseTokenAndNotificationTypeId> FirebaseTokenAndNotificationTypeIds
        {
            get
            {
                return _firebaseTokenAndNotificationTypeIds;
            }
        }

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
            var ft = new FirebaseTokenAndNotificationTypeId(evt.FirebaseToken, evt.NotificationTypeId);
            if (_firebaseTokenAndNotificationTypeIds.Contains(ft) == false)
            {
                _firebaseTokenAndNotificationTypeIds.Add(ft);
            }
        }

        public void Apply(FailedToSendFirebaseMessage evt)
        {
        }

        public void Apply(SentFirebaseMessage evt)
        {
            _sentMessageIds.Add(evt.MessageId);
        }

        public void Apply(FirebaseTokenRemoved evt)
        {
            var ft = new FirebaseTokenAndNotificationTypeId(evt.FirebaseToken, evt.NotificationTypeId);
            _firebaseTokenAndNotificationTypeIds.RemoveAll(t => t.Equals(ft));
        }

        public void AddToken(string fireBaseToke, string notificationTypeId)
        {
            var ft = new FirebaseTokenAndNotificationTypeId(fireBaseToke, notificationTypeId);

            if (_firebaseTokenAndNotificationTypeIds.Contains(ft) == false)
            {
                var evt = new FirebaseTokenAdded(PersonalNumber, fireBaseToke, notificationTypeId);
                Publish(evt);
            }
        }

        public void RemoveToken(string fireBaseToken, string notificationTypeId)
        {
            var ft = new FirebaseTokenAndNotificationTypeId(fireBaseToken, notificationTypeId);
            if (_firebaseTokenAndNotificationTypeIds.Contains(ft))
            {
                var evt = new FirebaseTokenRemoved(PersonalNumber, fireBaseToken, notificationTypeId);
                Publish(evt);
            }
        }

        public void SendMessage(string messageId, string message, string notificationTypeId)
        {
            if (_sentMessageIds.Contains(messageId))
            {
                // this is for duplicate send protection
                return;
            }

            var fts = _firebaseTokenAndNotificationTypeIds.Where(t => t.NotificationTypeId == notificationTypeId).ToList();
            foreach (var firebaseToken in fts)
            {
                var response = _firebaseNotificationSender.SendNotification(firebaseToken.FirebaseToken, message);
                if (response.WasSent)
                {
                    var evt = new SentFirebaseMessage(PersonalNumber, messageId, message, notificationTypeId);
                    Publish(evt);
                }
                else
                {
                    if (response.ShouldBeDeleted)
                    {
                        RemoveToken(firebaseToken.FirebaseToken, firebaseToken.NotificationTypeId);
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