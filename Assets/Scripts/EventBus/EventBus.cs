using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[UsedImplicitly]
public class EventBus
{
    private readonly Dictionary<Type, IEventHandlerCollection> _handlers=new();

    public void Subscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (!_handlers.ContainsKey(eventType))
        {
            _handlers.Add(eventType,new EventHandlerCollection<T>());
        }
        
        _handlers[eventType].Subscribe(handler);
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            handlers.Unsubscribe(handler);
        }
    }

    public void RaiseEvent<T>(T evt)
    {
        Type eventType = typeof(T);

        if (!_handlers.TryGetValue(eventType, out var handlers))
        {
            Debug.Log($"Not found subscriber of type: {eventType}");
            return;
        }
        handlers.RaiseEvent(evt);
    }
}