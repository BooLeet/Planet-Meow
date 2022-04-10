using System;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public abstract class Enemy : Character
{
    private Poolable poolable;
    public float followDistance = 0;
    public float distanceSpeedMultiplier = 1.3f;
    public float speedMultiplierDistance = 5;

    private Vector2 targetCoordinates;

    public bool isActive
    {
        get;
        private set;
    }

    [Header("Respawn")]
    public float respawnTime = 5;
    public float respawnPause;
    public float respawnDistance = 5;
    private float timer;

    public static event Action<Enemy> OnDeathStatic;
    public event Action OnDespawn;
    public event Action OnSpawn;

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
        if (isActive && movement.GetDistance(movement.currentCoordinates, targetCoordinates) < respawnDistance)
        {
            return;
        }

        timer += deltaTime;
        if (timer > respawnTime && isActive)
        {
            isActive = false;
            OnDespawn?.Invoke();
        }

        if (timer > respawnTime + respawnPause)
        {
            poolable.Enpool();
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
        isActive = true;
        timer = 0;
        EnemyRegistry.Register(this);
        OnSpawn?.Invoke();
    }

    public void UpdateTargetPosition(Vector2 coordinates)
    {
        if (!isActive)
        {
            StopMovement();
            return;
        }

        targetCoordinates = coordinates;
        movement.SetTrueBearing(movement.GetBearing(coordinates));
        float distance = movement.GetDistance(movement.currentCoordinates, coordinates);

        movement.movementSpeed = distance < followDistance? 0 : moveSpeed * Mathf.Lerp(1, distanceSpeedMultiplier, Mathf.InverseLerp(followDistance, speedMultiplierDistance, distance));
    }

    public void StopMovement()
    {
        movement.movementSpeed = 0;
    }
}
