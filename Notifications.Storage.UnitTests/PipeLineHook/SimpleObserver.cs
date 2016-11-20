namespace Notifications.Domain.UnitTests.PipeLineHook
{
    using System;

    using Notifications.Messages;

    public class SimpleObserver : IObserver<Event>
    {
        public void OnNext(Event value)
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}