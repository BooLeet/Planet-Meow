using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [System.Serializable]
    public class Setting
    {
        public string key;
        public string[] values;
        public int defaultValueIndex;
        public SettingApplier applier;
    }

    public Setting[] settings;
    public SettingsContainer settingsContainer
    {
        get;
        private set;
    }

    public event Action OnSettingsChanged;

    private void Start()
    {
        settingsContainer = SettingsContainer.Load();
        foreach (Setting setting in settings)
        {
            if (!settingsContainer.settings.TryGetValue(setting.key, out _))
            {
                settingsContainer.settings.Add(setting.key, setting.values[setting.defaultValueIndex]);
            }
        }
        ApplyAllSettings();
    }

    public void SetSetting(string key, string val)
    {
        if (settingsContainer.settings.TryGetValue(key, out _))
        {
            settingsContainer.settings.Remove(key);
        }

        settingsContainer.settings.Add(key, val);
        settingsContainer.Save();

        ApplySetting(key, val);
        OnSettingsChanged?.Invoke();
    }

    public Setting GetSetting(string key)
    {
        foreach (Setting setting in settings)
        {
            if(setting.key == key)
            {
                return setting;
            }
        }

        return null;
    }

    public string GetSettingValue(string key)
    {
        settingsContainer.settings.TryGetValue(key, out string value);
        return value;
    }

    private void ApplySetting(string key, string val)
    {
        foreach (Setting setting in settings)
        {
            if (setting.key == key)
            {
                setting.applier?.ApplySetting(val);

                return;
            }
        }
    }

    private void ApplyAllSettings()
    {
        foreach (var setting in settingsContainer.settings)
        {
            ApplySetting(setting.Key, setting.Value);
        }
    }
}
