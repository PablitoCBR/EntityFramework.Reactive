using EntityFramework.Reactive.ChangeDetection.Changes.Publishing;
using EntityFramework.Reactive.ChangeDetection.Changes.Subscriptions;

namespace EntityFramework.Reactive.ChangeDetection.Changes;

public interface IChangeDetectionOrchiestrator
{
    ChangeContext Begin();
}

public class ChangeDetectionOrchiestrator : IChangeDetectionOrchiestrator, IChangePublisher, IChangeObservable
{
    private readonly object _subscribingLock = new();
    private readonly Dictionary<Type, List<IChangeSubscriber>> _typedChangeSubscribers = new();

    public ChangeContext Begin()
    {
        return new ChangeContext(this);
    }

    public async ValueTask Publish(ChangeContext context)
    {
        var entityChangesGroupedByType = context.ChangedEntities.GroupBy(change => change.EntityType);

        foreach (var changesGroup in entityChangesGroupedByType)
        {
            if (_typedChangeSubscribers.TryGetValue(changesGroup.Key, out var subcribers) is false)
            {
                // no subscribers for given entity type
                continue;
            }

            var changes = changesGroup.SelectMany(changedEntity => changedEntity.Changes);

            foreach (var subscriber in subcribers)
            {
                foreach (var change in changes)
                {
                    await subscriber.OnChangeAsync(change);
                }
            }
        }
    }

    public IDisposable Subscribe(Type subscribedType, IChangeSubscriber subscriber)
    {
        lock (_subscribingLock)
        {
            if (_typedChangeSubscribers.TryGetValue(subscribedType, out var typedSubscribers) is false)
            {
                typedSubscribers = new List<IChangeSubscriber>();
                _typedChangeSubscribers.Add(subscribedType, typedSubscribers);
            }

            typedSubscribers.Add(subscriber);
        }

        return new ChangeDetectionUnsubscriber(subscribedType, subscriber, Unsubscribe);
    }

    public void Unsubscribe(Type subscribedType, IChangeSubscriber subscriber)
    {
        lock (_subscribingLock)
        {
            if (_typedChangeSubscribers.TryGetValue(subscribedType, out var typedSubscribers) is false)
            {
                return;
            }

            typedSubscribers.Remove(subscriber);

            if (typedSubscribers.Count == 0)
            {
                _typedChangeSubscribers.Remove(subscribedType);
            }
        }
    }
}
