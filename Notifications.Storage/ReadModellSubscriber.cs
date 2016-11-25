using Notifications.Domain.ReadModell;

namespace Notifications.Storage
{
    using System;

    using Domain;

    using Messages.Events;

    using NEventStore;

    public class ReadModellSubscriber : IObserver<ICommit>
    {
        private readonly IReadModell _readModell;

        public ReadModellSubscriber(IReadModell readModell)
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