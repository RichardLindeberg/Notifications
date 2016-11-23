using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.Person
{
    using System.Collections.Generic;
    using System.Linq;

    using Messages;

    using NUnit.Framework;

    using Should;

    public class TestPersonDataOneTokenOneNotificationTypeId
    {
        public const string Pno = "8004120351";

        public const string Token = "ABCDE";

        public const string NotificationTypeId = "Not1";
    }

    [TestFixture]
    public class GivenTokenAddedRemovedThenAdded : PersonTestBase
    {
        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            CreateSut(TestPersonDataOneTokenOneNotificationTypeId.Pno);
        }

        public override IEnumerable<PersonEvent> GetEvents()
        {
            yield return new FirebaseTokenAdded(TestPersonDataOneTokenOneNotificationTypeId.Pno, TestPersonDataOneTokenOneNotificationTypeId.Token, TestPersonDataOneTokenOneNotificationTypeId.NotificationTypeId);
            yield return new FirebaseTokenRemoved(TestPersonDataOneTokenOneNotificationTypeId.Pno, TestPersonDataOneTokenOneNotificationTypeId.Token, TestPersonDataOneTokenOneNotificationTypeId.NotificationTypeId);
            yield return new FirebaseTokenAdded(TestPersonDataOneTokenOneNotificationTypeId.Pno, TestPersonDataOneTokenOneNotificationTypeId.Token, TestPersonDataOneTokenOneNotificationTypeId.NotificationTypeId);
        }

        [Test]
        public void ShouldHaveOneToken()
        {
            Sut.FirebaseTokenAndNotificationTypeIds.LongCount().ShouldEqual(1);
        }

        [Test]
        public void ShouldHaveCorrectToke()
        {
            Sut.FirebaseTokenAndNotificationTypeIds.ShouldContain(new FirebaseTokenAndNotificationTypeId(TestPersonDataOneTokenOneNotificationTypeId.Token, TestPersonDataOneTokenOneNotificationTypeId.NotificationTypeId));
        }
    }
}