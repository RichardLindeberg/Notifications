using Notifications.Domain.NotificationSender;

namespace Notifications.Domain.UnitTests.Person
{
    using System.Linq;

    using Moq;

    using Notifications.Messages.Events.Person;

    using NUnit.Framework;

    using Should;

    using Person = Notifications.Domain.Person;

    public class WhenAddingSendingMessageToOneTokenAndFails
    {
        private const string Pno = "800412XXX";

        private const string Token = "ABCDE";

        private const string Not = "Not1";

        private const string MessageText = "Test Message";

        private Person _sut;

        private Mock<IFirebaseNotificationSender> _firebaseSender;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _firebaseSender = new Mock<IFirebaseNotificationSender>(MockBehavior.Strict);
            _firebaseSender.Setup(t => t.SendNotification(Token, MessageText))
                .Returns(new FireBaseNotificationResponse(false, true, "Error message", string.Empty));
            _sut = new Person(Pno, _firebaseSender.Object);
            _sut.AddToken(Token, Not);
            _sut.SendMessage("MessageId-1", MessageText, Not);
        }

        [Test]
        public void ShouldHaveCalledSendMessage()
        {
            _firebaseSender.Verify(t => t.SendNotification(Token, MessageText), Times.Once);
        }

        [Test]
        public void ShouldHaveMessageSentFailedEvent()
        {
            _sut.NewEvents.Any(t => t is FailedToSendFirebaseMessage).ShouldBeTrue();
        }

        [Test]
        public void ShouldHaveSavedMessageTextInSentEvent()
        {
            _sut.NewEvents.Any(t => (t is FailedToSendFirebaseMessage) && (string.CompareOrdinal(((FailedToSendFirebaseMessage)t).Message, MessageText) == 0)).ShouldBeTrue();
        }

        [Test]
        public void ShouldHaveNoToken()
        {
            _sut.FirebaseTokenAndNotificationTypeIds.ShouldBeEmpty();
        }
    }
}