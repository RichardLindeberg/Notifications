using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.PipeLineHook
{
    using System;
    using Domain;
    using Messages;
    using Moq;
    using NEventStore;
    using NEventStore.Client;

    using Notifications.Storage;

    using NUnit.Framework;

    public class WhenAddingToStore
    {
        private PipeLineHook pipeLineHook;

        private IStoreEvents _store;

        private Mock<IObserver<ICommit>> _observer;

        [OneTimeSetUp]
        protected void Setup()
        {

            _observer = new Mock<IObserver<ICommit>>(MockBehavior.Loose);
            pipeLineHook = new PipeLineHook();
            pipeLineHook.Subscribe(_observer.Object);
            _store = Wireup.Init()
                .UsingInMemoryPersistence()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(pipeLineHook)
                .Build();

            var pc = new PollingClient(_store.Advanced);
            var test = pc.ObserveFrom(null);

            var stream = _store.CreateStream("TestStream");
            stream.Add(new EventMessage() {Body = new PersonEvent("800412XXXX")});
            stream.CommitChanges(Guid.NewGuid());
        }

        [Test]
        public void ShouldHaveSentOneEvent()
        {
            _observer.Verify(t => t.OnNext(It.IsAny<ICommit>()), Times.Once);
        }

        [Test]
        public void ShouldNotHaveCalledCompleted()
        {
            _observer.Verify(t => t.OnCompleted(), Times.Never);
        }

        [Test]
        public void ShouldNotHaveCalledError()
        {
            _observer.Verify(t => t.OnError(It.IsAny<Exception>()), Times.Never);
        }

    }
}
