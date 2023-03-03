using EntityFramework.Reactive.ChangeDetection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFramework.Reactive.ChangeDetection.Changes;

public class ChangeContext
{
    private readonly IChangePublisher _changePublisher;

    public ChangeContext(ChangeDetectionOrchiestrator orchiestrator)
    {
        ChangedEntities = Array.Empty<EntityEntryChangeData>();
        _changePublisher = orchiestrator;
    }

    public IReadOnlyCollection<EntityEntryChangeData> ChangedEntities { get; private set; }

    public void DetectChanges(DbContext context)
    {
        ChangedEntities = context.ChangeTracker.Entries()
            .Where(entityEntry => entityEntry.State is not EntityState.Detached and not EntityState.Unchanged)
            .Select(entityEntry => new EntityEntryChangeData(GetChanges(entityEntry)))
            .ToList();
    }

    public void Flush()
    {
        _changePublisher.Publish(this);
    }

    private static IReadOnlyCollection<EntityPropertyChangeData> GetChanges(EntityEntry entityEntry)
    {
        // TODO: Handle entities addition
        if (entityEntry.State is not EntityState.Modified)
        {
            return Array.Empty<EntityPropertyChangeData>();
        }

        return entityEntry.Properties
            .Where(prop => prop.IsModified)
            .Select(prop => new EntityPropertyChangeData(prop.Metadata, prop.CurrentValue, prop.OriginalValue))
            .ToList();
    }
}
