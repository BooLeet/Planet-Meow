using UnityEngine;

public abstract class SettingApplier : ScriptableObject
{
    public abstract void ApplySetting(string value);
}
