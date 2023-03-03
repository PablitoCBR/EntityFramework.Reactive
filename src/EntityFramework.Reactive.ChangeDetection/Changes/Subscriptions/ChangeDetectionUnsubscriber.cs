using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Reactive.ChangeDetection.Changes.Subscriptions;
public class ChangeDetectionUnsubscriber : IDisposable
{
    private readonly Type _subscribedType;
    private readonly IChangeSubscriber _changeSubscriber;
    private readonly Action<Type, IChangeSubscriber> _unsubscribeCallback;

    public ChangeDetectionUnsubscriber(Type subscribedType, IChangeSubscriber subscriber, Action<Type, IChangeSubscriber> unsubscribeCallback)
    {
        _changeSubscriber = subscriber;
        _subscribedType = subscribedType;
        _unsubscribeCallback = unsubscribeCallback;
    }

    public void Dispose()
    {
        _unsubscribeCallback.Invoke(_subscribedType, _changeSubscriber);
        GC.SuppressFinalize(this);
    }
}
