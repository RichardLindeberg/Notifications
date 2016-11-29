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
    using Notifications.Domain.Subscription;
    using Notifications.Storage;
    using Notifications.Storage.ReadModells;
    using Notifications.Storage.Subscriptions;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class WhenAddingToStore
    {
        private PipeLineHook pipeLineHook;

        private IStoreEvents _store;

        private Mock<ISubscriptionConsumer> _readModell;

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

            _readModell = new Mock<ISubscriptionConsumer>();

            esf.CreateSubscription(_readModell.Object);

            var personExecutor = new PersonExecutor(_store, null);
            personExecutor.Execute("8004120351", person => person.AddToken("token", "not"), Guid.NewGuid());
            
        }

        [Test]
        public void ShouldHaveOneToken()
        {
            _readModell.Verify(rm => rm.NewEvent(It.IsAny<FirebaseTokenAdded>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddingAgainShouldHavTwo()
        {

            var personExecutor = new PersonExecutor(_store, null);
            personExecutor.Execute("8004120351", person => person.RemoveToken("token", "not"), Guid.NewGuid());
            Thread.Sleep(100);
            _readModell.Verify(rm => rm.NewEvent(It.IsAny<FirebaseTokenRemoved>(), It.IsAny<string>()), Times.Once);
        }

    }
}
