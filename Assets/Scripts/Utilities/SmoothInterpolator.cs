using System;
using UnityEngine;

public class SmoothInterpolator : MonoBehaviour
{
    private float time;
    private float timeCounter;
    private Action<float> valueChanger;
    private Action OnComplete;
    private Action OnInterupt;
    private bool complete = false;

    public static SmoothInterpolator StartInterpolation(GameObject targetObject,float time, Action<float> valueChanger, Action OnComplete = null, Action OnInterupt = null)
    {
        SmoothInterpolator interpolator = targetObject.AddComponent<SmoothInterpolator>();
        interpolator.time = time;
        interpolator.valueChanger = valueChanger;
        interpolator.OnComplete = OnComplete;
        interpolator.OnInterupt = OnInterupt;

        return interpolator;
    }

    private void Update()
    {
        if (complete)
        {
            return;
        }

        timeCounter += Time.deltaTime;
        float parameter = Mathf.Clamp01(timeCounter / time);

        valueChanger?.Invoke(parameter);

        if (timeCounter > time)
        {
            complete = true;
            OnComplete?.Invoke();
            Destroy(this);
        }
    }

    public void Interupt()
    {
        complete = true;
        OnInterupt?.Invoke();
        Destroy(this);
    }
}
