using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SphericalMovement movement;
    public float moveSpeed = 5;
    public float smoothTurnParameter = 10;
    private float targetBearing;
    private float currentBearing;

    void Start()
    {
        
    }


    void Update()
    {
        currentBearing = Mathf.LerpAngle(currentBearing, targetBearing, Time.deltaTime * smoothTurnParameter);
        movement.SetConditionalBearingDegrees(currentBearing);
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
        movement.movementSpeed = inputMagnitude * moveSpeed;
    }
}
