namespace Notifications.Storage
{
    using System;
    using System.Linq;

    using NEventStore;

    using Notifications.Domain;
    using Notifications.Messages;

    public class PersonExecutor : IPersonExecutor
    {
        public const string StreamName = "Person-";
        private readonly IStoreEvents _store;

        private readonly IFirebaseNotificationSender _firebaseNotificationSender;

        public PersonExecutor(IStoreEvents store, IFirebaseNotificationSender firebaseNotificationSender)
        {
            _store = store;
            _firebaseNotificationSender = firebaseNotificationSender;
        }

        public void Execute(string personalNumber, Action<Person> action, Guid commitId)
        {
            var person = new Person(personalNumber, _firebaseNotificationSender);
            var stream = _store.OpenStream($"{StreamName}-{personalNumber}");
            var events = stream.CommittedEvents.Where(t => t.Body is PersonEvent).Select(t => t.Body as PersonEvent);
            person.Apply(events);
            action(person);
            var eventsToStore = person.NewEvents.Select(t => new EventMessage() { Body = t });
            foreach (var eventMessage in eventsToStore)
            {
                stream.Add(eventMessage);
            }
            stream.CommitChanges(commitId);
        }
    }
}