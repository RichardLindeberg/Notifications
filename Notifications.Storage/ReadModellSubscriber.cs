using Notifications.Domain.ReadModell;

namespace Notifications.Storage
{
    using System;

    using Domain;

    using Messages.Events;

    using NEventStore;

    using Notifications.Storage.ReadModells;

    public class ReadModellSubscriber : IObserver<ICommit>
    {
        private readonly IReadModellWriter _readModellWriter;

        public ReadModellSubscriber(IReadModellWriter readModellWriter)
        {
            _readModellWriter = readModellWriter;
        }

        public void OnNext(ICommit commit)
        {
            foreach (var evt in commit.Events)
            {
                var eventToApply = evt.Body as Event;
                if (eventToApply != null)
                {
                    _readModellWriter.NewEvent(eventToApply, commit.CommitId.ToString());
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