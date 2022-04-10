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

    private Vector2 _currentCoordinates;
    public Vector2 currentCoordinates
    {
        get
        {
            return _currentCoordinates;
        }
        private set
        {
            _currentCoordinates = value;
            OnCoordinatesChanged?.Invoke();
        }
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

    private bool coordinatesSet = false;

    private void Start()
    {
        if (!coordinatesSet)
        {
            SetLat(startingCoordinates.x);
            SetLon(startingCoordinates.y);
        }

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

        Vector2 newCoordinates = GetDestination(bearing, distance);

        float finalBearing = (GetBearing(newCoordinates, currentCoordinates) + PI) % (2 * PI);
        azimuthCorrection += finalBearing - bearing;

        SetTrueBearing(currentConditionalBearing + azimuthCorrection);

        currentCoordinates = newCoordinates;
    }

    public Vector2 GetForwardCoordinates(float distance)
    {
        return GetDestination(currentBearing, distance);
    }

    public Vector2 GetDestination(float bearing, float distance)
    {
        float angle = distance / sphereRadius;
        return GetDestination(currentCoordinates, bearing, angle);
    }

    public static Vector2 GetDestination(Vector2 start ,float bearing, float angle)
    {
        float latDestination = Asin(Sin(start.x) * Cos(angle) + Cos(start.x) * Sin(angle) * Cos(bearing));
        float lonDestination = start.y + Atan2(Sin(bearing) * Sin(angle) * Cos(start.x), Cos(angle) - Sin(start.x) * Sin(latDestination));
        lonDestination = (lonDestination + 3 * PI) % (2 * PI) - PI;

        return new Vector2(latDestination, lonDestination);
    }

    public static float TrimAngleRad(float val)
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

    public float GetBearing(Vector2 target)
    {
        return GetBearing(currentCoordinates, target);
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
        coordinatesSet = true;
        Vector3 temp = currentCoordinates;
        temp.x = val;
        currentCoordinates = temp;
    }

    public void SetLon(float val)
    {
        coordinatesSet = true;
        Vector3 temp = currentCoordinates;
        temp.y = val;
        currentCoordinates = temp;
    }

    public void InvokeOnCollision(SphericalMovement other)
    {
        OnCollision?.Invoke(other);
    }

    public Vector3 GetCarthesianPosition()
    {
        return new Vector3(
            Cos(lat) * Sin(lon),
            Sin(lat),
            Cos(lat) * Cos(lon));
    }

    public static Vector3 GetCarthesianPosition(float lat, float lon)
    {
        return new Vector3(
            Cos(lat) * Sin(lon),
            Sin(lat),
            Cos(lat) * Cos(lon));
    }
}
