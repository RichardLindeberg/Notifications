namespace Notifications.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using NEventStore;

    using Notifications.Domain;
    using Notifications.Domain.ReadModell;
    using Notifications.Domain.Subscription;
    using Notifications.Storage.ReadModells;
    using Notifications.Storage.Subscriptions;

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

        public static ICollection<EventMessage> LoadStream(string streamName)
        {
            var stream = store.OpenStream(streamName);
            return stream.CommittedEvents;

        }

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
                var log = NLog.LogManager.GetCurrentClassLogger();
                log.Trace("Setting up FakeDi");
                var sqlConStr = ConfigurationManager.ConnectionStrings["Notifications"].ConnectionString;
                
                
                pipleLineHook = new PipeLineHook();
                store = new EventStoreFactory(pipleLineHook).GetStore();
                var toReadModellSubscriptionConsumer = new AddRemoveTokenToReadModellSubscriptionConsumer(sqlConStr);
                

                eventstoreSubscriptionFactory = new EventstoreSubscriptionFactory(store, pipleLineHook);
                eventstoreSubscriptionFactory.CreateSubscription(toReadModellSubscriptionConsumer);
                personExecutor = new PersonExecutor(store, null);
                personCommandHandler = new PersonCommandHandler(personExecutor, null);

                personalNumberAndTokenReadModell = new PersonalNumberAndTokenReadModell(sqlConStr);

                var multiActiveTokenProhibiter = new MultiActiveTokenProhibiter(new SubscriptionConsumerCommiter(sqlConStr), personalNumberAndTokenReadModell, personCommandHandler);
                eventstoreSubscriptionFactory.CreateSubscription(multiActiveTokenProhibiter);

                isReady = true;
                log.Trace("FakeDI ready");
            }
        }
    }
}