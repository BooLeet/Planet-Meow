using System;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public abstract class Enemy : Character
{
    private Poolable poolable;
    public float followDistance = 0;
    public static event Action<Enemy> OnDeathStatic;

    protected override void Start()
    {
        base.Start();
        poolable = GetComponent<Poolable>();
        OnDeath += HandleDeath;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        OnDeathStatic?.Invoke(this);
        poolable.Enpool();
    }

    public void Unregister()
    {
        EnemyRegistry.Unregister(this);
    }

    public void Register()
    {
        damageable.Revive();
        EnemyRegistry.Register(this);
    }

    public void UpdateTargetPosition(Vector2 coordinates)
    {
        movement.SetTrueBearing(movement.GetBearing(coordinates));
        float distance = movement.GetDistance(movement.currentCoordinates, coordinates);
        movement.movementSpeed = distance < followDistance? 0 : moveSpeed;
    }

    public void StopMovement()
    {
        movement.movementSpeed = 0;
    }
}
