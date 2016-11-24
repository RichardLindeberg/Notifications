using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.PipeLineHook
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain;

    using Messages;

    using Moq;

    using NEventStore;

    using Notifications.Storage;

    using NUnit.Framework;

    //public class WhenHavingPollingClient
    //{
    //    private PipeLineHook _pipeLineHook;

    //    private IStoreEvents _store;

    //    private Mock<IObserver<ICommit>> _observer;

    //    [OneTimeSetUp]
    //    public void Setup()
    //    {
    //        // THis Could be way nicer!
    //        _observer = new Mock<IObserver<ICommit>>(MockBehavior.Loose);
    //        _pipeLineHook = new PipeLineHook();
    //        _store = Wireup.Init()
    //            .UsingInMemoryPersistence()
    //            .UsingJsonSerialization()
    //            .HookIntoPipelineUsing(_pipeLineHook)
    //            .Build();

    //        var stream = _store.CreateStream("TestStream");
    //        stream.Add(new EventMessage() { Body = new PersonEvent("800412XXXX") });
    //        stream.CommitChanges(Guid.NewGuid());

    //        var subscriber = new EventstoreSubscriptionFactory(_store, _pipeLineHook);
    //        subscriber.CreateSubscription(_observer.Object, null, 1);
    //    }

    //    [Test]
    //    public void ShouldHaveSentOneEvent()
    //    {
    //        // Ugly but I can't come up with a solution now.
    //        Thread.Sleep(100);
    //        _observer.Verify(t => t.OnNext(It.IsAny<ICommit>()), Times.Once);
    //    }

    //    [Test]
    //    public void ShouldHaveSentTwoAfterSecondCommit()
    //    {
    //        var stream = _store.OpenStream("TestStream");
    //        stream.Add(new EventMessage() { Body = new PersonEvent("800412XXXX") });
    //        stream.CommitChanges(Guid.NewGuid());
    //        _observer.Verify(t => t.OnNext(It.IsAny<ICommit>()), Times.Exactly(2));
    //    }

    //    [Test]
    //    public void ShouldNotHaveCalledCompleted()
    //    {
    //        _observer.Verify(t => t.OnCompleted(), Times.Never);
    //    }

    //    [Test]
    //    public void ShouldNotHaveCalledError()
    //    {
    //        _observer.Verify(t => t.OnError(It.IsAny<Exception>()), Times.Never);
    //    }
    //}
}