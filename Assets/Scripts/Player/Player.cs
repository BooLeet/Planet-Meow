using System;
using UnityEngine;

public class Player : Character
{
    [Header("Movement")]
    public float smoothTurnParameter = 10;
    public float smoothMoveParameter = 10;
    private float targetBearing;
    private float currentBearing;
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

    public event Action OnAttack;

    void Update()
    {
        if (isDead)
        {
            return;
        }

        currentBearing = Mathf.LerpAngle(currentBearing, targetBearing, Time.deltaTime * smoothTurnParameter);
        movement.SetConditionalBearingDegrees(currentBearing);
        movement.movementSpeed = Mathf.Lerp(movement.movementSpeed, targetMoveSpeed, Time.deltaTime * smoothMoveParameter);
    }

    public void Move(Vector2 input)
    {
        float inputMagnitude = Mathf.Clamp01(input.magnitude);
        if (inputMagnitude == 0)
        {
            movement.movementSpeed = 0;
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
        projectile.Setup(this, attackDamage, projectileSpeed, projectileLifeTime, movement.GetForwardCoordinates(projectileSpawnDistance), movement.currentBearing);

        canAttack = false;
        SmoothInterpolator.StartInterpolation(gameObject, attackDelay, null, AllowAttack);

        OnAttack?.Invoke();
    }

    private void AllowAttack()
    {
        canAttack = true;
    }
}
