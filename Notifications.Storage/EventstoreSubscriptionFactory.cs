using Notifications.Domain.ReadModell;

namespace Notifications.Storage
{
    using System;

    using NEventStore;
    using NEventStore.Client;

    using Notifications.Domain;
    using Notifications.Storage.ReadModells;

    public class EventstoreSubscriptionFactory
    {
        private readonly IStoreEvents _store;

        private readonly IPipeLineHook _pipeLineHook;

        public EventstoreSubscriptionFactory(IStoreEvents store, IPipeLineHook pipeLineHook)
        {
            _store = store;
            _pipeLineHook = pipeLineHook;
        }

        public void CreateSubscription(IReadModellWriter readModellWriter, int interval = 5000)
        {
            ReadModellSubscriber readModellSubscriber = new ReadModellSubscriber(readModellWriter);
            StartSubscribing(readModellSubscriber, readModellWriter.GetLastCommit(), interval);
        }

        private void StartSubscribing(IObserver<ICommit> observer, string startAtCommit = null, int interval = 5000)
        {
            var pc = new PollingClient(_store.Advanced, interval);
            var commitObserver = pc.ObserveFrom(startAtCommit);

            _pipeLineHook.Subscribe(commitObserver);
            commitObserver.Subscribe(observer);
            commitObserver.Start();
        }
    }
}