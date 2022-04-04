using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    public Player model;
    [Header("Rotation")]
    public float forwardCoordinatesDistance = 0.2f;
    public float smoothRotationParameter = 10;
    private float targetBearing;
    private float currentBearing;

    [Header("Animation")]
    public Animator animator;
    public string[] attackTriggers;
    public string deathTrigger;
    private int attackTriggerIndex;

    [Header("Audio")]
    public Poolable damageAudioPrefab;
    public Poolable attackAudioPrefab;

    public Poolable damageEffectPrefab;

    void Start()
    {
        model.OnAttack += OnAttack;
        model.OnAttackBearingChanged += OnAttackBearingChanged;
        model.damageable.OnDamageTaken += OnDamageTaken;
        model.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        model.OnAttack -= OnAttack;
        model.OnAttackBearingChanged -= OnAttackBearingChanged;
        model.damageable.OnDamageTaken -= OnDamageTaken;
        model.OnDeath -= OnDeath;
    }

    private void Update()
    {
        if (model.isDead)
        {
            return;
        }
        currentBearing = Mathf.LerpAngle(currentBearing, targetBearing, Time.deltaTime * smoothRotationParameter);
        UpdateRotation();
    }

    private void OnAttack()
    {
        animator.SetTrigger(attackTriggers[attackTriggerIndex]);
        attackTriggerIndex = (attackTriggerIndex + 1) % attackTriggers.Length;
        
        ObjectSpawner.SpawnObject(attackAudioPrefab, transform.position);
    }

    private void OnAttackBearingChanged()
    {
        targetBearing = model.currentAttackBearing;
    }

    private void UpdateRotation()
    {
        Vector2 forwardCoordinates = model.movement.GetDestination(currentBearing, forwardCoordinatesDistance);
        Vector3 forward = SphericalMovement.GetCarthesianPosition(forwardCoordinates.x, forwardCoordinates.y);
        Vector3 up = model.movement.GetCarthesianPosition();

        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(forward, up), up);
    }

    private void OnDamageTaken()
    {
        ObjectSpawner.SpawnObject(damageAudioPrefab, transform.position);
        ObjectSpawner.SpawnObject(damageEffectPrefab, transform.position);
    }

    private void OnDeath()
    {
        animator.SetTrigger(deathTrigger);
    }
}
