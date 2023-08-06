using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Movement")]
    public float smoothTurnParameter = 10;
    public float smoothMoveParameter = 10;
    private float targetBearing;
    public float currentMovementBearing
    {
        get;
        private set;
    }
    private float targetMoveSpeed;

    [Header("Attack")]
    public float attackDamage = 10;
    public float projectileSpeed = 10;
    public float projectileLifeTime = 1;
    public float projectileSpawnDistance = 0.4f;
    public float attackDelay = 0.3f;
    public ObjectPool projectilePool;
    public bool canAttack
    {
        get;
        private set;
    } = true;

    public float currentAttackBearing
    {
        get;
        private set;
    }

    public bool isTargetingEnemy
    {
        get;
        private set;
    }

    public event Action OnAttack;
    public event Action OnAttackBearingChanged;

    protected override void Start()
    {
        base.Start();
        OnDeath += StopMovement;
    }

    protected override void OnDestroy()
    {
        OnDeath -= StopMovement;
        base.OnDestroy();
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        currentMovementBearing = Mathf.LerpAngle(currentMovementBearing, targetBearing, Time.deltaTime * smoothTurnParameter);
        movement.SetConditionalBearingDegrees(currentMovementBearing);
        movement.movementSpeed = Mathf.Lerp(movement.movementSpeed, targetMoveSpeed, Time.deltaTime * smoothMoveParameter);
    }

    public void Move(Vector2 input)
    {
        float inputMagnitude = Mathf.Clamp01(input.magnitude);
        if (inputMagnitude == 0)
        {
            targetMoveSpeed = movement.movementSpeed = 0;
            return;
        }

        input.Normalize();
        targetBearing = Vector2.SignedAngle(Vector2.up, input);
        targetMoveSpeed = inputMagnitude * moveSpeed;
    }

    public void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Projectile projectile = projectilePool.Depool().GetComponent<Projectile>();
        projectile.Setup(this, attackDamage, projectileSpeed, projectileLifeTime, movement.GetDestination(currentAttackBearing, projectileSpawnDistance), currentAttackBearing);

        canAttack = false;
        SmoothInterpolator.StartInterpolation(gameObject, attackDelay, null, AllowAttack);

        OnAttack?.Invoke();
    }

    public void AutoAim()
    {
        HashSet<Enemy> enemies = EnemyRegistry.GetEnemies();
        if (enemies == null || enemies.Count == 0)
        {
            isTargetingEnemy = false;
            return;
        }

        Enemy closestEnemy = null;
        float distance = float.MaxValue;
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.isActive)
            {
                continue;
            }
            float currentDistance = movement.GetDistance(movement.currentCoordinates, enemy.movement.currentCoordinates);
            if (currentDistance < distance)
            {
                closestEnemy = enemy;
                distance = currentDistance;
            }
        }

        isTargetingEnemy = true;
        SetAttackBearing(movement.GetBearing(closestEnemy.movement.currentCoordinates));
    }

    public void ManualAim(Vector2 input)
    {
        if (input.magnitude == 0)
        {
            SetAttackBearing(currentMovementBearing * Mathf.Deg2Rad + movement.azimuthCorrection);
            return;
        }

        float angle = Vector2.SignedAngle(Vector2.up, input)  * Mathf.Deg2Rad + movement.azimuthCorrection;
        SetAttackBearing(angle);
    }

    private void SetAttackBearing(float val)
    {
        currentAttackBearing = val;
        OnAttackBearingChanged?.Invoke();
    }

    private void AllowAttack()
    {
        canAttack = true;
    }

    private void StopMovement()
    {
        movement.movementSpeed = 0;
    }
}
