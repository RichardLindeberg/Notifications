namespace Notifications.Storage
{
    using System;
    using System.Configuration;

    using NEventStore;

    using Notifications.Domain;
    using Notifications.Domain.ReadModell;
    using Notifications.Storage.ReadModells;

    public static class FakedDi
    {
        private static readonly object LockObject = new object();

        private static PipeLineHook pipleLineHook;

        private static IStoreEvents store;

        private static EventstoreSubscriptionFactory eventstoreSubscriptionFactory;

        private static PersonExecutor personExecutor;

        private static bool isReady = false;

        private static PersonCommandHandler personCommandHandler;

        private static IPersonalNumberAndTokenReadModell personalNumberAndTokenReadModell;

        public static PersonCommandHandler GetPersonCommandHandler
        {
            get
            {
                if (isReady == false)
                {
                    SetUp();
                }
                return personCommandHandler;
            }
        }

        public static IPersonalNumberAndTokenReadModell PersonalNumberAndTokenReadModell
        {
            get
            {
                if (isReady == false)
                {
                    SetUp();
                }
                return personalNumberAndTokenReadModell;
            }
        }

        private static void SetUp()
        {
            lock (LockObject)
            {
                var sqlConStr = ConfigurationManager.ConnectionStrings["Notifications"].ConnectionString;
                
                pipleLineHook = new PipeLineHook();
                store = new EventStoreFactory(pipleLineHook).GetStore();
                var allTokensReadModellWriter = new AllTokensReadModellWriterWriter(sqlConStr);
                

                eventstoreSubscriptionFactory = new EventstoreSubscriptionFactory(store, pipleLineHook);
                eventstoreSubscriptionFactory.CreateSubscription(allTokensReadModellWriter);
                personExecutor = new PersonExecutor(store, null);
                personCommandHandler = new PersonCommandHandler(personExecutor, null);

                personalNumberAndTokenReadModell = new PersonalNumberAndTokenReadModell(sqlConStr);

                isReady = true;
            }
        }
    }
}