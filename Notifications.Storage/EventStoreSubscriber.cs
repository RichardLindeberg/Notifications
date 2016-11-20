namespace Notifications.Storage
{
    using System;

    using NEventStore;
    using NEventStore.Client;

    public class EventStoreSubscriber
    {
        private readonly IStoreEvents _store;

        private readonly IPipeLineHook _pipeLineHook;

        public EventStoreSubscriber(IStoreEvents store, IPipeLineHook pipeLineHook)
        {
            _store = store;
            _pipeLineHook = pipeLineHook;
        }

        public void Subscribe(IObserver<ICommit> observer, string startAtCommit = null, int interval = 5000)
        {
            var pc = new PollingClient(_store.Advanced, interval);
            var commitObserver = pc.ObserveFrom(startAtCommit);
            var observerForPollingClient = new ObserverForPollingClient(commitObserver);
            _pipeLineHook.Subscribe(observerForPollingClient);
            commitObserver.Subscribe(observer);
            commitObserver.Start();
        }
    }
}