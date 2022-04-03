using System;
using UnityEngine;

[RequireComponent(typeof(SphericalMovement))]
[RequireComponent(typeof(Poolable))]
public class Projectile : MonoBehaviour
{
    public float damage;
    private Poolable poolable;
    private SphericalMovement sphericalMovement;
    private Character sender;

    private float timeCounter;

    public event Action OnCollision;
    public event Action OnLifeOver;

    void Awake()
    {
        poolable = GetComponent<Poolable>();
        sphericalMovement = GetComponent<SphericalMovement>();

        sphericalMovement.OnCollision += HandleCollision;
    }

    private void OnDestroy()
    {
        sphericalMovement.OnCollision -= HandleCollision;
    }

    public void Setup(Character sender, float damage, float speed, float lifeTime, Vector2 coordinates, float bearing)
    {
        this.sender = sender;
        this.damage = damage;

        sphericalMovement.SetLat(coordinates.x);
        sphericalMovement.SetLon(coordinates.y);
        sphericalMovement.startingCoordinates = coordinates;

        sphericalMovement.SetTrueBearing(bearing);

        sphericalMovement.movementSpeed = speed;

        timeCounter = lifeTime;
    }

    private void HandleCollision(SphericalMovement obj)
    {
        if (sender != null && obj.GetComponent<Character>() == sender)
        {
            return;
        }

        Damageable damageable = obj.GetComponent<Damageable>();

        if (damageable)
        {
            damageable.TakeDamage(damage);
        }

        OnCollision?.Invoke();
        poolable.Enpool();
    }

    private void Update()
    {
        if (timeCounter <= 0)
        {
            return;
        }

        timeCounter -= Time.deltaTime;
        if (timeCounter <= 0)
        {
            poolable.Enpool();
            OnLifeOver?.Invoke();
        }
    }
}
