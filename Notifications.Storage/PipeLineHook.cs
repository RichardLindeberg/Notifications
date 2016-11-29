namespace Notifications.Storage
{
    using System;
    using System.Collections.Generic;

    using NEventStore;
    using NEventStore.Client;

    using NLog;

    public class PipeLineHook : PipelineHookBase, IPipeLineHook
    {
        private readonly List<IObserveCommits> _messageObservers;
        
        private Logger log;

        public PipeLineHook()
        {
            _messageObservers = new List<IObserveCommits>();
            log = NLog.LogManager.GetCurrentClassLogger();
        }

        public override void PostCommit(ICommit committed)
        {
            base.PostCommit(committed);
            log.Info($"Post commit {committed.BucketId} {committed.StreamId}");
            foreach (var messageObserver in _messageObservers)
            {
                messageObserver.PollNow();
            }
        }

        public IDisposable Subscribe(IObserveCommits commitObserver)
        {
            if (!_messageObservers.Contains(commitObserver))
            {
                _messageObservers.Add(commitObserver);
            }

            return new Unsubscriber(_messageObservers, commitObserver);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserveCommits> _observers;
            private readonly IObserveCommits _observer;

            public Unsubscriber(List<IObserveCommits> observers, IObserveCommits observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if ((_observer != null) && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}