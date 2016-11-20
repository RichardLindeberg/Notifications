namespace Notifications.Domain
{
    using System;

    using NEventStore;

    using Notifications.Messages;

    public class PeopleReadModellsSubscriber : IObserver<ICommit>
    {
        private readonly PersonalNumberAndTokenReadModell _readModell;

        public PeopleReadModellsSubscriber(PersonalNumberAndTokenReadModell readModell)
        {
            _readModell = readModell;
        }

        public void OnNext(ICommit commit)
        {
            foreach (var evt in commit.Events)
            {
                var eventToApply = evt.Body as Event;
                if (eventToApply != null)
                {
                    _readModell.Handle(eventToApply);
                }
            }
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {

        }
    }
}