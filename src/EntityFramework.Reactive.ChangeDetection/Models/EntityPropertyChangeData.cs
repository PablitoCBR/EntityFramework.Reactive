using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.Reactive.ChangeDetection.Models;

public class EntityPropertyChangeData
{
    public IProperty Property { get; }

    public object? NewValue { get; }
    public object? OriginalValue { get; }

    public EntityPropertyChangeData(IProperty property, object? newValue, object? originalValue)
    {
        Property = property;
        NewValue = newValue;
        OriginalValue = originalValue;
    }
}
