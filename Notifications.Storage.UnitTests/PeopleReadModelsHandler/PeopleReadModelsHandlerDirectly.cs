namespace Notifications.Domain.UnitTests.PeopleReadModelsHandler
{
    using Domain;

    using Messages;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class PeopleReadModelsHandlerDirectly
    {
        [Test]
        public void Add()
        {
            var sut = new PersonalNumberAndTokenReadModell();
            sut.PeopleWithTokens.ShouldBeEmpty();

            var addEvt = new FirebaseTokenAdded("800412XXXX", "ABCD");
            sut.Handle(addEvt);
            sut.PeopleWithTokens.ShouldNotBeEmpty();
        }

        [Test]
        public void AddRemove()
        {
            var sut = new PersonalNumberAndTokenReadModell();
            sut.PeopleWithTokens.ShouldBeEmpty();

            var addEvt = new FirebaseTokenAdded("800412XXXX", "ABCD");
            sut.Handle(addEvt);

            var removeEvt = new FirebaseTokenRemoved("800412XXXX", "ABCD");
            sut.Handle(removeEvt);
            sut.PeopleWithTokens.ShouldBeEmpty();

        }

        [Test]
        public void AddToTokensSamePnr()
        {
            var sut = new PersonalNumberAndTokenReadModell();
            sut.PeopleWithTokens.ShouldBeEmpty();

            var addEvt = new FirebaseTokenAdded("800412XXXX", "ABCD");
            var addEvt2 = new FirebaseTokenAdded("800412XXXX", "ABCDE");

            sut.Handle(addEvt);
            sut.Handle(addEvt2);

            sut.PeopleWithTokens.Count.ShouldEqual(2);

        }

        [Test]
        public void AddToTokensSamePnrRemoveOne()
        {
            var sut = new PersonalNumberAndTokenReadModell();
            sut.PeopleWithTokens.ShouldBeEmpty();

            var addEvt = new FirebaseTokenAdded("800412XXXX", "ABCD");
            var addEvt2 = new FirebaseTokenAdded("800412XXXX", "ABCDE");

            sut.Handle(addEvt);
            sut.Handle(addEvt2);

            var removeEvt = new FirebaseTokenRemoved("800412XXXX", "ABCD");
            sut.Handle(removeEvt);


            sut.PeopleWithTokens.Count.ShouldEqual(1);

        }
    }
}
