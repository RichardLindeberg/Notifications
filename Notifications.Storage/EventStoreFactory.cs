namespace Notifications.Storage
{
    using NEventStore;
    using NEventStore.Persistence.Sql.SqlDialects;

    using Notifications.Storage.EventStore.Logging.NLog;

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
                //.UsingInMemoryPersistence()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(_pipeLineHook)
                .LogTo(t => new NLogLogger(t))
                .Build();
        }
        
    }
}
