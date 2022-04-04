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
    private int attackTriggerIndex;

    void Start()
    {
        model.OnAttack += OnAttack;
        model.OnAttackBearingChanged += OnAttackBearingChanged;
    }

    private void OnDestroy()
    {
        model.OnAttack -= OnAttack;
        model.OnAttackBearingChanged -= OnAttackBearingChanged;
    }

    private void Update()
    {
        currentBearing = Mathf.LerpAngle(currentBearing, targetBearing, Time.deltaTime * smoothRotationParameter);
        UpdateRotation();
    }

    private void OnAttack()
    {
        animator.SetTrigger(attackTriggers[attackTriggerIndex]);
        attackTriggerIndex = (attackTriggerIndex + 1) % attackTriggers.Length;
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
}
