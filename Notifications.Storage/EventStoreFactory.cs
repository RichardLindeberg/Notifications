using NEventStore.Persistence.Sql.SqlDialects;

namespace Notifications.Storage
{
    using NEventStore;

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
                .UsingSqlPersistence("Notifications").WithDialect(new MsSqlDialect())
                .InitializeStorageEngine()
                //.UsingInMemoryPersistence()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(_pipeLineHook)
                .Build();
        }
    }
}
