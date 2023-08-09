using System;

/// <summary>
/// Events can't be stored directly in collections, so this is solution
/// </summary>
public class EventWrapper
{
    private event Action callback;

    public void Invoke()
    {
        callback?.Invoke();
    }

    public void Subscribe(Action action)
    {
        callback += action;
    }

    public void Unsubscribe(Action action)
    {
        callback -= action;
    }
}

/// <summary>
/// Events can't be stored directly in collections, so this is solution
/// </summary>
public class EventWrapper<T>
{
    private event Action<T> callback;

    public void Invoke(T val)
    {
        callback?.Invoke(val);
    }

    public void Subscribe(Action<T> action)
    {
        callback += action;
    }

    public void Unsubscribe(Action<T> action)
    {
        callback -= action;
    }
}