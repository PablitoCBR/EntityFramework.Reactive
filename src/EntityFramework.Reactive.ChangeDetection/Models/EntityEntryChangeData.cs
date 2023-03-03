namespace EntityFramework.Reactive.ChangeDetection.Models;

public class EntityEntryChangeData
{
	public Type EntityType { get; }
	public IReadOnlyCollection<EntityPropertyChangeData> Changes { get; }

	public EntityEntryChangeData(Type entityType, IReadOnlyCollection<EntityPropertyChangeData> changes)
	{
		EntityType = entityType;
		Changes = changes;
	}
}
