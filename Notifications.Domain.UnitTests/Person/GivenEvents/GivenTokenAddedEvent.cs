namespace Notifications.Domain.UnitTests.Person
{
    using System.Collections.Generic;

    using Notifications.Messages;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class GivenTokenAddedEvent : PersonTestBase
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
        }

        [Test]
        public void ShouldHaveOneToken()
        {
            Sut.FirebaseTokens.Count.ShouldEqual(1);
        }

        [Test]
        public void ShouldHaveCorrectToke()
        {
            Sut.FirebaseTokens.ShouldContain(_token);
        }
    }
}