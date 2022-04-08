using System;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(SphericalMovement))]
public abstract class Character : MonoBehaviour
{
    private Damageable _damageable;
    public Damageable damageable
    {
        get
        {
            if (_damageable == null)
            {
                _damageable = GetComponent<Damageable>();
            }

            return _damageable;
        }
    }

    private SphericalMovement _movement;
    public SphericalMovement movement
    {
        get
        {
            if (_movement == null)
            {
                _movement = GetComponent<SphericalMovement>();
            }

            return _movement;
        }
    }

    public float moveSpeed = 5;

    public bool isDead
    {
        get => damageable.isDead;
    }

    public event Action OnDeath;

    protected virtual void Start()
    {
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
