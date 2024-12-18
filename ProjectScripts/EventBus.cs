using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<object, bool> Walking;
    public static event Action<object> Attacking;
    public static event Action<object, bool> Falling;
    public static event Action<object> Created;

    public static void OnWalking(object obj, bool isWalking)
    {
        Walking?.Invoke(obj, isWalking);
    }
    public static void OnAttacking(object obj)
    {
        Attacking?.Invoke(obj);
    }
    public static void OnFalling(object obj, bool isFalling)
    {
        Falling?.Invoke(obj, isFalling);
    }
    public static void OnCreated(object obj)
    {
        Created?.Invoke(obj);
    }
}
