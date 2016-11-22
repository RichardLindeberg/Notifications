using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.Person
{
    using System.Linq;

    using Notifications.Messages;

    using NUnit.Framework;

    using Should;

    using Person = Notifications.Domain.Person;

    [TestFixture]
    public class WhenAddingAndRemovingToken
    {
        private const string Pno = "800412XXX";

        private const string Token = "ABCDE";

        private Person _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _sut = new Person(Pno, null);
            _sut.AddToken(Token);
            _sut.RemoveToken(Token);
        }

        [Test]
        public void ShouldNotHaveAnyToken()
        {
            _sut.FirebaseTokens.Count.ShouldEqual(0);
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