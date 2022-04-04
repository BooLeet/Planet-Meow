using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class Enemy : Character
{
    private Poolable poolable;

    protected override void Start()
    {
        base.Start();
        poolable = GetComponent<Poolable>();
        OnDeath += poolable.Enpool;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnDeath -= poolable.Enpool;
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
        movement.movementSpeed = moveSpeed;
    }

    public void StopMovement()
    {
        movement.movementSpeed = 0;
    }
}
