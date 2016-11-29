namespace Notifications.Storage.Subscriptions
{
    using System;

    using NEventStore;
    using NEventStore.Client;

    using Notifications.Domain.Subscription;

    public class EventstoreSubscriptionFactory
    {
        private readonly IStoreEvents _store;

        private readonly IPipeLineHook _pipeLineHook;

        public EventstoreSubscriptionFactory(IStoreEvents store, IPipeLineHook pipeLineHook)
        {
            _store = store;
            _pipeLineHook = pipeLineHook;
        }

        public void CreateSubscription(ISubscriptionConsumer subscriptionConsumer, int interval = 5000)
        {
            ReadModellSubscriber readModellSubscriber = new ReadModellSubscriber(subscriptionConsumer);
            StartSubscribing(readModellSubscriber, subscriptionConsumer.GetCheckPointToken(), interval);
        }

        private void StartSubscribing(IObserver<ICommit> observer, string checkPointToken = null, int interval = 5000)
        {
            var pc = new PollingClient(_store.Advanced, interval);
            var commitObserver = pc.ObserveFrom(checkPointToken);

            _pipeLineHook.Subscribe(commitObserver);
            commitObserver.Subscribe(observer);
            commitObserver.Start();
        }
    }
}