namespace Notifications.Domain.UnitTests.Person.GivenEvents
{
    using System.Collections.Generic;
    using System.Linq;

    using Messages.Events.Person;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class GivenTokenAddedEvent : PersonTestBase
    {
        private const string Token = "ABCDE";

        private const string Pno = "800412XXX";

        private const string Not = "not1";

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            CreateSut(Pno);            
        }

        public override IEnumerable<PersonEvent> GetEvents()
        {
            yield return new FirebaseTokenAdded(Pno, Token, Not);
        }

        [Test]
        public void ShouldHaveOneToken()
        {
            Sut.FirebaseTokenAndNotificationTypeIds.Count().ShouldEqual(1);
        }

        [Test]
        public void ShouldHaveCorrectToke()
        {
            Sut.FirebaseTokenAndNotificationTypeIds.ShouldContain(new FirebaseTokenAndNotificationTypeId(Token, Not));
        }
    }
}