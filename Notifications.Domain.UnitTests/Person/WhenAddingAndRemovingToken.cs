using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.Person
{
    using System.Linq;

    using NUnit.Framework;

    using Should;

    using Person = Notifications.Domain.Person;

    [TestFixture]
    public class WhenAddingAndRemovingToken
    {
        private const string Token = "ABCDE";

        private const string Pno = "800412XXX";

        private const string Not = "not1";

        private Person _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _sut = new Person(Pno, null);
            _sut.AddToken(Token, Not);
            _sut.RemoveToken(Token, Not);
        }

        [Test]
        public void ShouldNotHaveAnyToken()
        {
            _sut.FirebaseTokenAndNotificationTypeIds.Count().ShouldEqual(0);
        }

        [Test]
        public void ShouldHaveTwoNewEvent()
        {
            _sut.NewEvents.Count().ShouldEqual(2);
        }

        [Test]
        public void ShouldHaveAnTokenAddedEvent()
        {
            _sut.NewEvents.Any(t => t is FirebaseTokenAdded).ShouldBeTrue();
        }

        [Test]
        public void ShouldHaveAnTokenRemovedEvent()
        {
            _sut.NewEvents.Any(t => t is FirebaseTokenRemoved).ShouldBeTrue();
        }
    }
}