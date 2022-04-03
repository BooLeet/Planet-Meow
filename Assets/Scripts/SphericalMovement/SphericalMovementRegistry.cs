using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalMovementRegistry : MonoBehaviour
{
    public static SphericalMovementRegistry instance
    {
        get;
        private set;
    }

    private List<SphericalMovement> registeredMovements = new List<SphericalMovement>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < registeredMovements.Count; ++i)
        {
            SphericalMovement currentMovement = registeredMovements[i];
            currentMovement.MoveForward(currentMovement.movementSpeed * Time.fixedDeltaTime);
            if (!currentMovement.allowCorrection)
            {
                continue;
            }

            for (int j = 0; j < registeredMovements.Count; ++j)
            {
                if (i == j)
                {
                    continue;
                }
                SphericalMovement otherMovement = registeredMovements[j];

                float distance = currentMovement.GetDistance(currentMovement.currentCoordinates, otherMovement.currentCoordinates);
                if (distance >= currentMovement.radius + otherMovement.radius)
                {
                    continue;
                }

                float correctionBearing = SphericalMovement.GetBearing(otherMovement.currentCoordinates, currentMovement.currentCoordinates);
                float correctionDistance = currentMovement.radius + otherMovement.radius - distance;

                currentMovement.Move(correctionBearing, correctionDistance);
            }
        }
    }

    public static SphericalMovementRegistry GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("[SphericalMovementRegistry]");
            instance = obj.AddComponent<SphericalMovementRegistry>();
        }

        return instance;
    }

    public static void Register(SphericalMovement movement)
    {
        GetInstance().registeredMovements.Add(movement);
    }

    public static void Unregister(SphericalMovement movement)
    {
        if (instance == null)
        {
            return;
        }

        instance.registeredMovements.Remove(movement);
    }
}
