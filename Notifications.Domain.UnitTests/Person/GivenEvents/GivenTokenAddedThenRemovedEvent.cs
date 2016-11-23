using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.Person
{
    using System.Collections.Generic;

    using Messages;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class GivenTokenAddedThenRemovedEvent : PersonTestBase
    {
        private string _token;

        private string _pno;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            _pno = "800412XXXX";
            _token = "ABCDE";
            CreateSut(_pno);
        }

        public override IEnumerable<PersonEvent> GetEvents()
        {
            yield return new FirebaseTokenAdded(_pno, _token);
            yield return new FirebaseTokenRemoved(_pno, _token);
        }

        [Test]
        public void ShouldHaveZeroTokens()
        {
            Sut.FirebaseTokens.ShouldBeEmpty();
        }
    }
}