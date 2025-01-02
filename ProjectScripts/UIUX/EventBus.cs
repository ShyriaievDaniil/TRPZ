using System;

public static class EventBus
{
    public static event Action<object, bool> Walking;
    public static event Action<object> Attacking;
    public static event Action<object, bool> Falling;
    public static event Action<object, string> Created;
    public static event Action<object, object> Summon;
    public static event Action<object> Jumping;
    public static event Action<object, int> LevelUp;
    public static event Action<object> EnemyDeath;
    public static event Action<object> PlayerDeath;
    public static event Action<object, float> HealthChanged;
    public static event Action<float> ManaChanged;
    public static event Action<float> ExpChanged;

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
    public static void OnCreated(object obj, string type)
    {
        Created?.Invoke(obj, type);
    }
    public static void OnSummon(object obj, object summoner)
    {
        Summon?.Invoke(obj, summoner); 
    }
    public static void OnJumping(object obj)
    {
        Jumping?.Invoke(obj);
    }
    public static void OnLevelUp(object obj, int level)
    {
        LevelUp?.Invoke(obj, level); 
    }
    public static void OnDeath(object obj)
    {
        if (obj is Enemy) {
            EnemyDeath?.Invoke(obj);
        }
        if (obj is Player)
        {
            PlayerDeath?.Invoke(obj);
        }
    }
    public static void OnHealthChanged(object obj, float percentage)
    {
        HealthChanged?.Invoke(obj, percentage);
    }
    public static void OnManaChanged(float percentage)
    {
        ManaChanged?.Invoke(percentage);
    }
    public static void OnExpChanged(float percentage)
    {
        ExpChanged?.Invoke(percentage);
    }
}
