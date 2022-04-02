using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalMovementCameraPresenter : MonoBehaviour
{
    public SphericalMovement model;
    public Vector3 sphereCenter;
    public float cameraHeight = 8;
    public float cameraBearing;
    public float forwardCoordinatesDistance = 0.2f;
    public float smoothingParameter = 10;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        model.OnCoordinatesChanged += OnCoordinatesChanged;
        OnCoordinatesChanged();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothingParameter);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothingParameter);
    }

    private void OnDestroy()
    {
        model.OnCoordinatesChanged -= OnCoordinatesChanged;
    }

    private void OnCoordinatesChanged()
    {
        targetPosition = sphereCenter + (model.sphereRadius + cameraHeight) * GetCarthesianPosition(model.lat, model.lon);

        Vector2 forwardCoordinates = model.GetDestinationCoordinates(cameraBearing * Mathf.Deg2Rad + model.azimuthCorrection, forwardCoordinatesDistance);
        Vector3 forward = GetCarthesianPosition(forwardCoordinates.x, forwardCoordinates.y);
        Vector3 up = GetCarthesianPosition(model.lat, model.lon);

        targetRotation = Quaternion.LookRotation(-up, Vector3.ProjectOnPlane(forward, up));
    }

    private Vector3 GetCarthesianPosition(float lat, float lon)
    {
        return new Vector3(
            Mathf.Cos(lat) * Mathf.Sin(lon),
            Mathf.Sin(lat),
            Mathf.Cos(lat) * Mathf.Cos(lon));
    }
}
