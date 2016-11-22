using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Messages.Events.Person;

namespace Notifications.Domain.UnitTests.Person
{
    using NUnit.Framework;

    using Domain;

    using Notifications.Messages;

    using Should;

    [TestFixture]
    public class WhenAddingToken
    {
        private const string Pno = "800412XXX";

        private const string Token = "ABCDE";

        private Person _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _sut = new Person(Pno, null);
            _sut.AddToken(Token);
        }

        [Test]
        public void ShouldHaveOneToken()
        {
            _sut.FirebaseTokens.Count.ShouldEqual(1);
        }

        [Test]
        public void ShouldHaveCorrectToken()
        {
            _sut.FirebaseTokens.ShouldContain(Token);
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
