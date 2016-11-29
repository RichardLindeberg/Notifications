using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.PipeLineHook
{
    using System;
    using System.Threading;

    using Domain;
    using Messages;
    using Moq;
    using NEventStore;
    using NEventStore.Client;

    using Notifications.Domain.ReadModell;
    using Notifications.Storage;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class WhenAddingToStore
    {
        private PipeLineHook pipeLineHook;

        private IStoreEvents _store;

        private PeopleReadModell _readModell;

        [OneTimeSetUp]
        protected void Setup()
        {
            pipeLineHook = new PipeLineHook();
            _store = Wireup.Init()
                .UsingInMemoryPersistence()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(pipeLineHook)
                .Build();

            var esf = new EventstoreSubscriptionFactory(_store, pipeLineHook);

            _readModell = new PeopleReadModell();

            esf.CreateSubscription(_readModell);

            var personExecutor = new PersonExecutor(_store, null);
            personExecutor.Execute("8004120351", person => person.AddToken("token", "not"), Guid.NewGuid());
            
        }

        [Test]
        public void ShouldHaveOneToken()
        {
            
            _readModell.PeopleWithTokens.ShouldBeEmpty();
        }

        [Test]
        public void AddingAgainShouldHavTwo()
        {

            var personExecutor = new PersonExecutor(_store, null);
            personExecutor.Execute("8004120351", person => person.AddToken("token", "not2"), Guid.NewGuid());
            Thread.Sleep(100);
            _readModell.PeopleWithTokens.Count.ShouldEqual(2);
        }

    }
}
