using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(SphericalMovement))]
public abstract class Character : MonoBehaviour
{
    private Damageable damageable;
    [HideInInspector] public SphericalMovement movement;
    public float moveSpeed = 5;

    public bool isDead
    {
        get => damageable.isDead;
    }

    public event Action OnDeath;

    protected virtual void Start()
    {
        damageable = GetComponent<Damageable>();
        movement = GetComponent<SphericalMovement>();
        damageable.OnDeath += HandleDeath;
    }

    protected virtual void OnDestroy()
    {
        damageable.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        OnDeath?.Invoke();
    }
}
