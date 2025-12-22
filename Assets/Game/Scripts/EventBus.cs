using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> _subscribers = new();

    public static void Subscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (!_subscribers.ContainsKey(type))
            _subscribers[type] = new List<Delegate>();

        _subscribers[type].Add(callback);
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (_subscribers.TryGetValue(type, out var list))
            list.Remove(callback);
    }

    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (!_subscribers.TryGetValue(type, out var list))
            return;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] is Action<T> callback)
                callback.Invoke(eventData);
        }
    }

    public static void Clear()
    {
        _subscribers.Clear();
    }
}
