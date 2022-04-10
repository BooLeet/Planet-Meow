using UnityEngine;

public abstract class BaseObjectLocalizer : MonoBehaviour
{
    protected virtual void Start()
    {
        Localizer.OnLanguageChanged += UpdateText;
        UpdateText();
    }

    protected virtual void OnDestroy()
    {
        Localizer.OnLanguageChanged -= UpdateText;
    }

    protected abstract void UpdateText();
}
