using Notifications.Domain.ReadModell;

namespace Notifications.Storage
{
    using System;

    using Domain;

    using Messages.Events;

    using NEventStore;

    using Notifications.Domain.Subscription;
    using Notifications.Storage.ReadModells;

    public class ReadModellSubscriber : IObserver<ICommit>
    {
        private readonly ISubscriptionConsumer _subscriptionConsumer;

        public ReadModellSubscriber(ISubscriptionConsumer subscriptionConsumer)
        {
            _subscriptionConsumer = subscriptionConsumer;
        }

        public void OnNext(ICommit commit)
        {
            foreach (var evt in commit.Events)
            {
                var eventToApply = evt.Body as Event;
                if (eventToApply != null)
                {
                    _subscriptionConsumer.NewEvent(eventToApply, commit.CheckpointToken);
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