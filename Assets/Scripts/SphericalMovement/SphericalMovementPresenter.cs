using UnityEngine;

public class SphericalMovementPresenter : MonoBehaviour
{
    public SphericalMovement model;
    public Vector3 sphereCenter;
    public float forwardCoordinatesDistance = 0.2f;

    void Start()
    {
        model.OnBearingChanged += OnBearingChanged;
        model.OnCoordinatesChanged += UpdatePosition;

        UpdatePosition();
        UpdateRotation();
    }

    private void OnDestroy()
    {
        model.OnBearingChanged -= OnBearingChanged;
        model.OnCoordinatesChanged -= UpdatePosition;
    }

    private void OnBearingChanged()
    {
        UpdateRotation();
    }

    public void UpdateRotation()
    {
        Vector2 forwardCoordinates = model.GetForwardCoordinates(forwardCoordinatesDistance);
        Vector3 forward = GetCarthesianPosition(forwardCoordinates.x, forwardCoordinates.y);
        Vector3 up = GetCarthesianPosition(model.lat, model.lon);

        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(forward, up), up);
    }

    public void UpdatePosition()
    {
        transform.position = sphereCenter + model.sphereRadius * GetCarthesianPosition(model.lat, model.lon);
    }

    private Vector3 GetCarthesianPosition(float lat, float lon)
    {
        return new Vector3(
            Mathf.Cos(lat) * Mathf.Sin(lon),
            Mathf.Sin(lat),
            Mathf.Cos(lat) * Mathf.Cos(lon));
    }
}
