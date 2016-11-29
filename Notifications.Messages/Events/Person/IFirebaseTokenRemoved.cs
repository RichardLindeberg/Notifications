namespace Notifications.Messages.Events.Person
{
    using System;

    public interface IFirebaseTokenRemoved : IEvent
    {
        string FirebaseToken { get; }

        string NotificationTypeId { get; }

        string PersonalNumber { get; }
    }
}