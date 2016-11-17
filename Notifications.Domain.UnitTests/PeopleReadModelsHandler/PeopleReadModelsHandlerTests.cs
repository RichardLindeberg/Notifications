using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.UnitTests.PeopleReadModelsHandler
{
    using Notifications.Messages;

    using NUnit.Framework;

    using Should;

    using Domain;

    [TestFixture]
    public class PeopleReadModelsHandlerTests
    {
        [Test]
        public void Add()
        {
            var sut = new PeopleReadModelsHandler();
            sut.PeopleWithTokens.ShouldBeEmpty();

            var addEvt = new FirebaseTokenAdded("800412XXXX", "ABCD");
            sut.Handle(addEvt);
            sut.PeopleWithTokens.ShouldNotBeEmpty();
        }

        [Test]
        public void AddRemove()
        {
            var sut = new PeopleReadModelsHandler();
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
            var sut = new PeopleReadModelsHandler();
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
            var sut = new PeopleReadModelsHandler();
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
