using EntityFramework.Reactive.ChangeDetection.Changes.Subscriptions;

namespace EntityFramework.Reactive.ChangeDetection.Changes.Publishing;

public interface IChangeObservable
{
    IDisposable Subscribe<T>(IChangeSubscriber subscriber) where T : class
        => Subscribe(typeof(T), subscriber);

    IDisposable Subscribe(Type subscribedType, IChangeSubscriber subscriber);
}
