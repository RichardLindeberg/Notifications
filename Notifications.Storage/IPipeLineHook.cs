namespace Notifications.Storage
{
    using System;

    using NEventStore;

    public interface IPipeLineHook : IPipelineHook
    {
        IDisposable Subscribe(IObserver<ICommit> observer);
    }
}