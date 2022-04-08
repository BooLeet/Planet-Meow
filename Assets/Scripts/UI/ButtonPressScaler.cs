using UnityEngine;

public class ButtonPressScaler : MonoBehaviour
{
    public RectTransform rectTransform;
    public float pressScale = 0.9f;
    public float time = 0.3f;
    private SmoothInterpolator lastInterpolator;

    private float targetScale = 1;
    private float currentScale = 1;

    public void OnPress()
    {
        StopInterpolator();
        targetScale = pressScale;
        lastInterpolator = SmoothInterpolator.StartInterpolation(gameObject, time, SetScale, OnInterupt: OnInterupt);
    }

    public void OnRelease()
    {
        StopInterpolator();
        targetScale = 1;
        lastInterpolator = SmoothInterpolator.StartInterpolation(gameObject, time, SetScale, OnInterupt: OnInterupt);
    }

    private void SetScale(float parameter)
    {
        currentScale = Mathf.Lerp(currentScale, targetScale, parameter);
        rectTransform.localScale = Vector3.one * currentScale;
    }

    private void OnDisable()
    {
        StopInterpolator();
    }

    private void StopInterpolator()
    {
        if (lastInterpolator != null)
        {
            lastInterpolator.Interupt();
        }
    }

    private void OnInterupt()
    {
        targetScale = 1;
        SetScale(1);
    }
}
