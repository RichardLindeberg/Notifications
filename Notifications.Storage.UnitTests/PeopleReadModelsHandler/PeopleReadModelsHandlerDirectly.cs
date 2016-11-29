using Notifications.Domain.ReadModell;

namespace Notifications.Storage.UnitTests.PeopleReadModelsHandler
{
    using Messages.Events.Person;

    using Notifications.Domain;

    using NUnit.Framework;

    using Should;

    //[TestFixture]
    //public class PeopleReadModelsHandlerDirectly
    //{
    //    private const string Token = "ABCDE";

    //    private const string Token2 = "XYZ";

    //    private const string Pno = "800412XXX";

    //    private const string Not = "not1";

    //    [Test]
    //    public void Add()
    //    {
    //        var sut = new PeopleReadModell();
    //        sut.PeopleWithTokens.ShouldBeEmpty();

    //        var addEvt = new FirebaseTokenAdded(Pno, Token, Not);
    //        sut.Handle(addEvt);
    //        sut.PeopleWithTokens.ShouldNotBeEmpty();
    //    }

    //    [Test]
    //    public void AddRemove()
    //    {
    //        var sut = new PeopleReadModell();
    //        sut.PeopleWithTokens.ShouldBeEmpty();

    //        var addEvt = new FirebaseTokenAdded(Pno, Token, Not);
    //        sut.Handle(addEvt);

    //        var removeEvt = new FirebaseTokenRemoved(Pno, Token, Not);
    //        sut.Handle(removeEvt);
    //        sut.PeopleWithTokens.ShouldBeEmpty();
    //    }

    //    [Test]
    //    public void AddToTokensSamePnr()
    //    {
    //        var sut = new PeopleReadModell();
    //        sut.PeopleWithTokens.ShouldBeEmpty();

    //        var addEvt = new FirebaseTokenAdded(Pno, Token, Not);
    //        var addEvt2 = new FirebaseTokenAdded(Pno, Token2, Not);

    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);

    //        sut.PeopleWithTokens.Count.ShouldEqual(2);
    //    }

    //    [Test]
    //    public void AddTwice()
    //    {
    //        var sut = new PeopleReadModell();
    //        sut.PeopleWithTokens.ShouldBeEmpty();

    //        var addEvt = new FirebaseTokenAdded(Pno, Token, Not);
    //        var addEvt2 = new FirebaseTokenAdded(Pno, Token, Not);

    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);

    //        sut.PeopleWithTokens.Count.ShouldEqual(1);
    //    }

    //    [Test]
    //    public void AddToTokensSamePnrRemoveOne()
    //    {
    //        var sut = new PeopleReadModell();
    //        sut.PeopleWithTokens.ShouldBeEmpty();

    //        var addEvt = new FirebaseTokenAdded(Pno, Token, Not);
    //        var addEvt2 = new FirebaseTokenAdded(Pno, Token2, Not);

    //        sut.Handle(addEvt);
    //        sut.Handle(addEvt2);

    //        var removeEvt = new FirebaseTokenRemoved(Pno, Token, Not);
    //        sut.Handle(removeEvt);


    //        sut.PeopleWithTokens.Count.ShouldEqual(1);
    //    }
    //}
}
