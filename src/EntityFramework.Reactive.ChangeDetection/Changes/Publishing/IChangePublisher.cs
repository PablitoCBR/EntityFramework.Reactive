namespace EntityFramework.Reactive.ChangeDetection.Changes.Publishing;

public interface IChangePublisher
{
    ValueTask Publish(ChangeContext context);
}
