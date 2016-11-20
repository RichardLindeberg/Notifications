namespace Notifications.Storage
{
    using System;

    using NEventStore;
    using NEventStore.Client;

    public class ObserverForPollingClient : IObserver<ICommit>
    {
        private readonly IObserveCommits _observeCommits;

        public ObserverForPollingClient(IObserveCommits observeCommits)
        {
            _observeCommits = observeCommits;

        }

        public void OnNext(ICommit value)
        {
            _observeCommits.PollNow();
        }

        public void OnError(Exception error)
        {
           
        }

        public void OnCompleted()
        {
            _observeCommits.Dispose();
        }
    }
}