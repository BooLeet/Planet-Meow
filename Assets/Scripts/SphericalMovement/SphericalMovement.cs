using System;
using UnityEngine;
using static UnityEngine.Mathf;

public class SphericalMovement : MonoBehaviour
{
    public float radius = 0.5f;
    public float sphereRadius = 5;
    public float movementSpeed = 0;
    public bool allowCorrection = false;
    public bool correctOthers = true;

    public Vector2 startingCoordinates;
    
    public Vector2 currentCoordinates
    {
        get;
        private set;
    }

    public float lat
    {
        get => currentCoordinates.x;
    }

    public float lon
    {
        get => currentCoordinates.y;
    }

    public float currentBearing
    {
        get;
        private set;
    }

    public float azimuthCorrection
    {
        get;
        private set;
    }

    public float currentConditionalBearing
    {
        get;
        private set;
    }

    public event Action OnCoordinatesChanged;
    public event Action OnBearingChanged;

    public event Action<SphericalMovement> OnCollision;

    private void Start()
    {
        SetLat(startingCoordinates.x);
        SetLon(startingCoordinates.y);

        SphericalMovementRegistry.Register(this);
    }

    private void OnDestroy()
    {
        SphericalMovementRegistry.Unregister(this);
    }

    public void MoveForward(float distance)
    {
        Move(currentBearing, distance);
    }

    public void Move(float bearing, float distance)
    {
        if (movementSpeed <= 0)
        {
            return;
        }

        Vector2 newCoordinates = GetDestinationCoordinates(bearing, distance);

        float finalBearing = (GetBearing(newCoordinates, currentCoordinates) + PI) % (2 * PI);
        azimuthCorrection += finalBearing - bearing;

        SetTrueBearing(currentConditionalBearing + azimuthCorrection);

        currentCoordinates = newCoordinates;
        OnCoordinatesChanged?.Invoke();
    }

    public Vector2 GetForwardCoordinates(float distance)
    {
        return GetDestinationCoordinates(currentBearing, distance);
    }

    public Vector2 GetDestinationCoordinates(float bearing, float distance)
    {
        float angle = distance / sphereRadius;
        float latDestination = Asin(Sin(lat) * Cos(angle) + Cos(lat) * Sin(angle) * Cos(bearing));
        float lonDestination = lon + Atan2(Sin(bearing) * Sin(angle) * Cos(lat), Cos(angle) - Sin(lat) * Sin(latDestination));
        lonDestination = (lonDestination + 3 * PI) % (2 * PI) - PI;

        return new Vector2(latDestination, lonDestination);
    }

    private static float TrimAngleRad(float val)
    {
        val %= 2 * PI;
        if (val < 0)
        {
            val += 2 * PI;
        }

        return val;
    }

    public static float GetBearing(Vector2 start, Vector2 end)
    {
        return TrimAngleRad(Atan2(Sin(end.y - start.y) * Cos(end.x), Cos(start.x) * Sin(end.x) - Sin(start.x) * Cos(end.x) * Cos(end.y - start.y)));
    }

    public float GetDistance(Vector2 start, Vector2 end)
    {
        float deltaLat = end.x - start.x;
        float deltaLon = end.y - start.y;
        float a = Pow(Sin(deltaLat / 2), 2) + Cos(start.x) * Cos(end.x) * Pow(Sin(deltaLon / 2), 2);
        float c = 2 * Atan2(Sqrt(a), Sqrt(1 - a));

        return sphereRadius * c;
    }

    public void SetTrueBearing(float val)
    {
        currentBearing = TrimAngleRad(val);
        currentConditionalBearing = currentBearing - azimuthCorrection;
        OnBearingChanged?.Invoke();
    }

    public void SetTrueBearingDegrees(float degrees)
    {
        SetTrueBearing(degrees * Deg2Rad);
    }

    public void SetConditionalBearing(float val)
    {
        SetTrueBearing(TrimAngleRad(val + azimuthCorrection));
    }

    public void SetConditionalBearingDegrees(float degrees)
    {
        SetConditionalBearing(degrees * Deg2Rad);
    }

    public void SetLat(float val)
    {
        Vector3 temp = currentCoordinates;
        temp.x = val;
        currentCoordinates = temp;
    }

    public void SetLon(float val)
    {
        Vector3 temp = currentCoordinates;
        temp.y = val;
        currentCoordinates = temp;
    }

    public void InvokeOnCollision(SphericalMovement other)
    {
        OnCollision?.Invoke(other);
    }
}
