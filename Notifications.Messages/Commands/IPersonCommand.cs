namespace Notifications.Messages.Commands
{
    using System;

    public interface IPersonCommand
    {
        Guid CommandId { get; }
        string PersonalNumber { get;  }
    }
}