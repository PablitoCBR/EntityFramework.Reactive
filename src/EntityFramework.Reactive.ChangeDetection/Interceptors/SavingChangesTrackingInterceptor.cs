using EntityFramework.Reactive.ChangeDetection.Changes;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EntityFramework.Reactive.ReinforcedChangeTracking.Interceptors;

public class SavingChangesTrackingInterceptor : ISaveChangesInterceptor
{
    private readonly IChangeDetectionOrchiestrator _orchiestrator;

    private ChangeContext? _changeContext;

    public SavingChangesTrackingInterceptor(IChangeDetectionOrchiestrator orchiestrator)
    {
        _orchiestrator = orchiestrator;
    }

    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void SaveChangesCanceled(DbContextEventData eventData)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesCanceledAsync(DbContextEventData eventData, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        _changeContext?.Flush();
        return result;
    }

    public ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        _changeContext?.Flush();
        return ValueTask.FromResult(result);
    }

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        _changeContext = _orchiestrator.Begin();
        _changeContext.DetectChanges(eventData.Context!);
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        _changeContext = _orchiestrator.Begin();
        _changeContext.DetectChanges(eventData.Context!);
        return ValueTask.FromResult(result);
    }
}
