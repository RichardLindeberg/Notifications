namespace Notifications.Domain
{
    using System;
    using System.Collections.Generic;

    using NEventStore;

    using NLog;

    public interface IPipeLineHook : IPipelineHook
    {
        IDisposable Subscribe(IObserver<ICommit> observer);
    }

    public class PipeLineHook : PipelineHookBase, IPipeLineHook
    {
        private readonly List<IObserver<ICommit>> _messageObservers;
        
        private Logger log;

        public PipeLineHook()
        {
            _messageObservers = new List<IObserver<ICommit>>();
            log = NLog.LogManager.GetCurrentClassLogger();
        }

        public override void PostCommit(ICommit committed)
        {
            base.PostCommit(committed);
            log.Info($"Post commit {committed.BucketId} {committed.StreamId}");
            foreach (var messageObserver in _messageObservers)
            {
                messageObserver.OnNext(committed);
            }
        }

        public IDisposable Subscribe(IObserver<ICommit> observer)
        {
            if (!_messageObservers.Contains(observer))
            {
                _messageObservers.Add(observer);
            }

            return new Unsubscriber(_messageObservers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<ICommit>> _observers;
            private readonly IObserver<ICommit> _observer;

            public Unsubscriber(List<IObserver<ICommit>> observers, IObserver<ICommit> observer)
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