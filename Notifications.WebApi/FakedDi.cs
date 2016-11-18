namespace Notifications.WebApi
{
    using NEventStore;

    using Notifications.Domain;

    public static class FakedDi
    {
        private static readonly object LockObject = new object();

        private static PipeLineHook pipleLineHook;

        private static IStoreEvents store;

        private static PersonalNumberAndTokenReadModell personalNumberAndTokenReadModell;

        private static EventStoreSubscriber eventStoreSubscriber;

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
                PeopleReadModellsSubscriber peopleReadModellsSubscriber =
                    new PeopleReadModellsSubscriber(personalNumberAndTokenReadModell);
                eventStoreSubscriber = new EventStoreSubscriber(store, pipleLineHook);
                eventStoreSubscriber.Subscribe(peopleReadModellsSubscriber);
                personExecutor = new PersonExecutor(store, null);
                personCommandHandler = new PersonCommandHandler(personExecutor, personalNumberAndTokenReadModell);
                isReady = true;
            }
        }
    }
}