namespace EntityFramework.Reactive.ChangeDetection.Changes;

public interface IChangeDetectionOrchiestrator
{
    ChangeContext Begin();
}

public interface IChangePublisher
{
    void Publish(ChangeContext context);
}

public class ChangeDetectionOrchiestrator : IChangeDetectionOrchiestrator, IChangePublisher
{
    public ChangeDetectionOrchiestrator()
    {
    }

    public ChangeContext Begin()
    {
        return new ChangeContext(this);
    }

    public void Publish(ChangeContext context)
    {
        // TODO
    }
}
