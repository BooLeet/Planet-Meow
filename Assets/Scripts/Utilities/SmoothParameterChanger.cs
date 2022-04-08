using UnityEngine;
using UnityEngine.Events;

public class SmoothParameterChanger : MonoBehaviour
{
    public float firstValue = 0;
    public float secondValue = 0;
    public float smoothTime = 0.3f;

    public UnityEvent<float> ChangeFunction;

    private SmoothInterpolator lastInterpolator;

    private float targetValue = 1;
    private float currentValue = 0;

    private void Awake()
    {
        currentValue = firstValue;
    }

    public void ChangeToSecondValue()
    {
        StopInterpolator();
        targetValue = secondValue;
        lastInterpolator = SmoothInterpolator.StartInterpolation(gameObject, smoothTime, SetValue);
    }

    public void ChangeToFirstValue()
    {
        StopInterpolator();
        targetValue = firstValue;
        lastInterpolator = SmoothInterpolator.StartInterpolation(gameObject, smoothTime, SetValue);
    }

    private void SetValue(float parameter)
    {
        currentValue = Mathf.Lerp(currentValue, targetValue, parameter);
        ChangeFunction?.Invoke(currentValue);
    }

    private void StopInterpolator()
    {
        if (lastInterpolator != null)
        {
            lastInterpolator.Interupt();
        }
    }
}
