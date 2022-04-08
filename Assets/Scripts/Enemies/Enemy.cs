using System;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public abstract class Enemy : Character
{
    private Poolable poolable;
    public float followDistance = 0;
    public float distanceSpeedMultiplier = 1.3f;
    public float speedMultiplierDistance = 5;
    public float respawnTime = 5;

    private float timer;

    public static event Action<Enemy> OnDeathStatic;
    public event Action OnDespawn;

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

    public void UpdateDespawnTimer(float deltaTime)
    {
        timer += deltaTime;
        if (timer > respawnTime)
        {
            poolable.Enpool();
            OnDespawn?.Invoke();
        }
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
        timer = 0;
        EnemyRegistry.Register(this);
    }

    public void UpdateTargetPosition(Vector2 coordinates)
    {
        movement.SetTrueBearing(movement.GetBearing(coordinates));
        float distance = movement.GetDistance(movement.currentCoordinates, coordinates);

        movement.movementSpeed = distance < followDistance? 0 : moveSpeed * Mathf.Lerp(1, distanceSpeedMultiplier, Mathf.InverseLerp(followDistance, speedMultiplierDistance, distance));
    }

    public void StopMovement()
    {
        movement.movementSpeed = 0;
    }
}
