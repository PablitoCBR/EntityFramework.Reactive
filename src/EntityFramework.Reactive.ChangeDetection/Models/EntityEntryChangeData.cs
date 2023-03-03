namespace EntityFramework.Reactive.ChangeDetection.Models;

public class EntityEntryChangeData
{
	public IReadOnlyCollection<EntityPropertyChangeData> Changes { get; }

	public EntityEntryChangeData(IReadOnlyCollection<EntityPropertyChangeData> changes)
	{
		Changes = changes;
	}
}
