namespace Notifications.Domain.UnitTests.PeopleReadModelsHandler
{
    using System;

    using NEventStore;

    using Notifications.Messages;

    using NUnit.Framework;

    using Should;

    using PipeLineHook = Notifications.Domain.PipeLineHook;

    [TestFixture]
    public class ReadModelsSubscriberTests
    {
        private IPipeLineHook _pipeLineHook;

        private IStoreEvents _store;

        private PersonalNumberAndTokenReadModell _sut;

        [SetUp]
        public void SetUp()
        {
            _pipeLineHook = new PipeLineHook();
            _store = Wireup.Init()
                .UsingInMemoryPersistence()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(_pipeLineHook)
                .Build();

            var subscriber = new EventStoreSubscriber(_store, _pipeLineHook);
            _sut = new PersonalNumberAndTokenReadModell();
            var readModelSubscriber = new PeopleReadModellsSubscriber(_sut);
            subscriber.Subscribe(readModelSubscriber, null, 10000);

            var stream = _store.CreateStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXX", "ABCDE") });
            stream.CommitChanges(Guid.NewGuid());

        }

        [Test]
        public void WhenOnlyFirstEvent()
        {
            _sut.PeopleWithTokens.Count.ShouldEqual(1); 
        }

        [Test]
        public void WhenAddingSameAgain()
        {
            var stream = _store.OpenStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXX", "ABCDE") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(1);
        }

        [Test]
        public void WhenAddingAnotherPersonSameToken()
        {
            var stream = _store.OpenStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXY", "ABCDE") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(2);
        }

        [Test]
        public void WhenAddingAnotherTokenSamePerson()
        {
            var stream = _store.OpenStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXX", "ABCDEF") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(2);
        }

        [Test]
        public void WhenRemoving()
        {
            var stream = _store.OpenStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenRemoved("800412XXXX", "ABCDE") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(0);
        }
    }
}