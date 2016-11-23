namespace Notifications.Domain.UnitTests.Person
{
    using System.Linq;

    using Domain;

    using Notifications.Messages.Events.Person;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class WhenAddingToken
    {
        private const string Pno = "800412XXX";

        private const string Token = "ABCDE";

        private const string Not = "Not1";

        private Person _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _sut = new Person(Pno, null);
            _sut.AddToken(Token, Not);
        }

        [Test]
        public void ShouldHaveOneToken()
        {
            _sut.FirebaseTokenAndNotificationTypeIds.Count().ShouldEqual(1);
        }

        [Test]
        public void ShouldHaveCorrectToken()
        {
            _sut.FirebaseTokenAndNotificationTypeIds.ShouldContain(new FirebaseTokenAndNotificationTypeId(Token, Not));
        }

        [Test]
        public void ShouldHaveOneNewEvent()
        {
            _sut.NewEvents.Count().ShouldEqual(1);
        }

        [Test]
        public void ShouldHaveAnTOkenAddedEvent()
        {
            _sut.NewEvents.Any(t => t is FirebaseTokenAdded).ShouldBeTrue();
        }
    }
}
