namespace Notifications.Domain.UnitTests.Person.GivenEvents
{
    using System.Collections.Generic;

    using Notifications.Messages.Events.Person;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class GivenTokenAddedThenRemovedEvent : PersonTestBase
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
            yield return new FirebaseTokenRemoved(Pno, Token, Not);
        }

        [Test]
        public void ShouldHaveZeroTokens()
        {
            Sut.FirebaseTokenAndNotificationTypeIds.ShouldBeEmpty();
        }
    }
}