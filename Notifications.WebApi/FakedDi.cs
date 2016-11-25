namespace Notifications.WebApi
{
    using NEventStore;

    using Notifications.Domain;
    using Notifications.Storage;

    public static class FakedDi
    {
        private static readonly object LockObject = new object();

        private static PipeLineHook pipleLineHook;

        private static IStoreEvents store;

        private static PersonalNumberAndTokenReadModell personalNumberAndTokenReadModell;

        private static PeopleReadModell peopleReadModell;

        private static EventstoreSubscriptionFactory eventstoreSubscriptionFactory;

        private static PersonExecutor personExecutor;

        private static bool isReady = false;

        private static PersonCommandHandler personCommandHandler;

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

        public static PeopleReadModell PeopleReadModell
        {
            get
            {
                if (isReady == false)
                {
                    SetUp();
                }
                return peopleReadModell;
            }
        }

        public static PersonalNumberAndTokenReadModell PersonalNumberAndTokenReadModell
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
                pipleLineHook = new PipeLineHook();
                store = new EventStoreFactory(pipleLineHook).GetStore();
                personalNumberAndTokenReadModell = new PersonalNumberAndTokenReadModell();
                peopleReadModell = new PeopleReadModell();

                eventstoreSubscriptionFactory = new EventstoreSubscriptionFactory(store, pipleLineHook);
                eventstoreSubscriptionFactory.CreateSubscription(personalNumberAndTokenReadModell);
                eventstoreSubscriptionFactory.CreateSubscription(peopleReadModell);
                personExecutor = new PersonExecutor(store, null);
                personCommandHandler = new PersonCommandHandler(personExecutor, personalNumberAndTokenReadModell);
                isReady = true;
            }
        }
    }
}