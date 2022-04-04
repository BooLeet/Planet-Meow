using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    [Header("Attack")]
    public float explosionDamage;

    private bool canDamage = true;

    protected override void Start()
    {
        base.Start();
        movement.OnCollision += OnCollision;
        damageable.OnRevive += OnRevive;
    }

    protected override void OnDestroy()
    {
        movement.OnCollision -= OnCollision;
        damageable.OnRevive -= OnRevive;
        base.OnDestroy();
    }

    private void OnCollision(SphericalMovement obj)
    {
        if (!canDamage)
        {
            return;
        }

        Player player = obj.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        canDamage = false;
        player.damageable.TakeDamage(explosionDamage);
        damageable.Kill();
    }

    private void OnRevive()
    {
        canDamage = true;
    }
}
