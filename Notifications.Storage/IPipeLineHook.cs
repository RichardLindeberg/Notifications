namespace Notifications.Storage
{
    using System;

    using NEventStore;
    using NEventStore.Client;

    public interface IPipeLineHook : IPipelineHook
    {
        IDisposable Subscribe(IObserveCommits commitObserver);
    }
}