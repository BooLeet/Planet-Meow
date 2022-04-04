using UnityEngine;

public class SphericalMovementPresenter : MonoBehaviour
{
    public SphericalMovement model;
    public Vector3 sphereCenter;
    public float forwardCoordinatesDistance = 0.2f;
    public bool presentRotation = true;

    void Start()
    {
        model.OnBearingChanged += UpdateTransform;
        model.OnCoordinatesChanged += UpdateTransform;

        UpdateTransform();
    }

    private void OnDestroy()
    {
        model.OnBearingChanged -= UpdateTransform;
        model.OnCoordinatesChanged -= UpdateTransform;
    }

    private void UpdateTransform()
    {
        UpdatePosition();
        UpdateRotation();
    }

    public void UpdateRotation()
    {
        if (!presentRotation)
        {
            return;
        }

        Vector2 forwardCoordinates = model.GetForwardCoordinates(forwardCoordinatesDistance);
        Vector3 forward = SphericalMovement.GetCarthesianPosition(forwardCoordinates.x, forwardCoordinates.y);
        Vector3 up = model.GetCarthesianPosition();

        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(forward, up), up);
    }

    public void UpdatePosition()
    {
        transform.position = sphereCenter + model.sphereRadius * model.GetCarthesianPosition();
    }
}
