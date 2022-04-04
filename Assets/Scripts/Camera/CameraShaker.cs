using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Transform cameraTransform;

    public float maxOffset = 0.1f;
    public float maxAngle = 2;
    [Space]
    public float shakeTime = 1;
    private float shakeTimeCounter = 0;
    [Space]

    public float newOffsetTimeDelay = 0.02f;
    private float delayTimeCounter;
    private Vector3 previousOffset;
    private Vector3 currentOffset;

    private float currentAngleSign = -1;
    private float previousAngle;
    private float currentAngle;

    private float currentStrength;


    public void Shake(float strength)
    {
        strength = Mathf.Clamp01(strength);
        if(shakeTimeCounter <= 0 || currentStrength < strength)
            currentStrength = strength;

        shakeTimeCounter = shakeTime;
        CalculateNewOffset();
    }

    private void Update()
    {
        if (shakeTimeCounter == 0)
            return;

        if (shakeTimeCounter < 0)
        {
            shakeTimeCounter = 0;
            delayTimeCounter = 0;
            previousAngle = 0;
            cameraTransform.localPosition = previousOffset = Vector3.zero;
            cameraTransform.localRotation = Quaternion.identity;
            return;
        }

        delayTimeCounter += Time.deltaTime;
        if (delayTimeCounter >= newOffsetTimeDelay)
        {
            CalculateNewOffset();
            delayTimeCounter %= newOffsetTimeDelay;
        }

        cameraTransform.localPosition = Vector3.Lerp(previousOffset, currentOffset, delayTimeCounter / newOffsetTimeDelay);
        cameraTransform.localRotation = Quaternion.Euler(previousAngle, currentAngle, delayTimeCounter / newOffsetTimeDelay);
        shakeTimeCounter -= Time.deltaTime;
    }

    private void CalculateNewOffset()
    {
        previousOffset = currentOffset;
        currentOffset = currentStrength * Random.insideUnitSphere.normalized * (shakeTimeCounter / shakeTime) * maxOffset;

        currentAngleSign *= -1;
        previousAngle = currentAngle;
        currentAngle = currentStrength * currentAngleSign * (shakeTimeCounter / shakeTime) * maxAngle;
    }
}
