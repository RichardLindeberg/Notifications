namespace Notifications.Storage
{
    using System;

    using NEventStore;
    using NEventStore.Client;

    using Notifications.Domain;

    public class EventstoreSubscriptionFactory
    {
        private readonly IStoreEvents _store;

        private readonly IPipeLineHook _pipeLineHook;

        public EventstoreSubscriptionFactory(IStoreEvents store, IPipeLineHook pipeLineHook)
        {
            _store = store;
            _pipeLineHook = pipeLineHook;
        }

        public void CreateSubscription(IReadModell readModell, string startAtCommit = null, int interval = 5000)
        {
            ReadModellSubscriber readModellSubscriber = new ReadModellSubscriber(readModell);
            StartSubscribing(readModellSubscriber, startAtCommit, interval);
        }

        private void StartSubscribing(IObserver<ICommit> observer, string startAtCommit = null, int interval = 5000)
        {
            var pc = new PollingClient(_store.Advanced, interval);
            var commitObserver = pc.ObserveFrom(startAtCommit);

            var observerForPollingClient = new ObserverForPollingClient(commitObserver);
            _pipeLineHook.Subscribe(observerForPollingClient);
            commitObserver.Subscribe(observer);
            commitObserver.Start();
//            commitObserver.PollNow();
        }
    }
}