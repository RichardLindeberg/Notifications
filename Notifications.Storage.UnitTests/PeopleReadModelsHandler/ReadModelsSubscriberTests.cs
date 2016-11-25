using Notifications.Domain.ReadModell;
using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.PeopleReadModelsHandler
{
    using System;

    using NEventStore;

    using Storage;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class ReadModelsSubscriberTests
    {
        private IPipeLineHook _pipeLineHook;

        private IStoreEvents _store;

        private PeopleReadModell _sut;

        [SetUp]
        public void SetUp()
        {
            _pipeLineHook = new PipeLineHook();
            _store = Wireup.Init()
                .UsingInMemoryPersistence()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(_pipeLineHook)
                .Build();

            var subscriber = new EventstoreSubscriptionFactory(_store, _pipeLineHook);
            _sut = new PeopleReadModell();
            subscriber.CreateSubscription(_sut, null, 10000);

            var stream = _store.CreateStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXX", "ABCDE", "not1") });
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
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXX", "ABCDE", "not1") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(1);
        }

        [Test]
        public void WhenAddingAnotherPersonSameToken()
        {
            var stream = _store.OpenStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXY", "ABCDE", "not1") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(2);
        }

        [Test]
        public void WhenAddingAnotherTokenSamePerson()
        {
            var stream = _store.OpenStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenAdded("800412XXXX", "ABCDEF", "not1") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(2);
        }

        [Test]
        public void WhenRemoving()
        {
            var stream = _store.OpenStream("TestStream");
            stream.Add(new EventMessage() { Body = new FirebaseTokenRemoved("800412XXXX", "ABCDE", "not1") });
            stream.CommitChanges(Guid.NewGuid());

            _sut.PeopleWithTokens.Count.ShouldEqual(0);
        }
    }
}