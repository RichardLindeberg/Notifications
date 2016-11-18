using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEventStore;
using NEventStore.Persistence.Sql;

namespace Notifications.Domain
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using NEventStore.Client;

    public class EventStoreFactory
    {
        private readonly IPipeLineHook _pipeLineHook;

        public EventStoreFactory(IPipeLineHook pipeLineHook)
        {
            _pipeLineHook = pipeLineHook;
        }

        public IStoreEvents GetStore()
        {
            return Wireup.Init()
                .UsingSqlPersistence(
                    "Persist Security Info=False;Integrated Security=SSPI;database=Notifications;server=(local)")
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(_pipeLineHook)
                .Build();
        }
    }

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
