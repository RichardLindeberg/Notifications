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
                //.UsingSqlPersistence(
                //    "Persist Security Info=False;Integrated Security=SSPI;database=Notifications;server=(local)").WithDialect(new MsSqlDialect())
                .UsingInMemoryPersistence()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(_pipeLineHook)
                .Build();
        }
    }
}
