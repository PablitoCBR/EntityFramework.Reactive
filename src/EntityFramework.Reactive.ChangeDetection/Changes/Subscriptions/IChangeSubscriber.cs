using EntityFramework.Reactive.ChangeDetection.Models;

namespace EntityFramework.Reactive.ChangeDetection.Changes.Subscriptions;

public interface IChangeSubscriber
{
    ValueTask OnChangeAsync(EntityPropertyChangeData changeData);
}
