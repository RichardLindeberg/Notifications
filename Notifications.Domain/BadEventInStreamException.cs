namespace Notifications.Domain
{
    using System;

    [Serializable]
    public class BadEventInStreamException : InvalidOperationException
    {
        public BadEventInStreamException()
        {
        }

        public BadEventInStreamException(string message)
            : base(message)
        {
        }

        public BadEventInStreamException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}