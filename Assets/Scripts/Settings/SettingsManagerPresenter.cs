using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManagerPresenter : MonoBehaviour
{
    public SettingsManager model;

    [System.Serializable]
    public struct SettingButtonInfo
    {
        public string settingKey;
        public SettingButton button;
    }

    public SettingButtonInfo[] buttonInfos;

    void Start()
    {
        
    }

    private void UpdateButtonTexts()
    {
        foreach (SettingButtonInfo buttonInfo in buttonInfos)
        {
            buttonInfo.button.SetSetting(model.GetSettingValue(buttonInfo.settingKey));
        }
    }

    public void SetValue(string key, string val)
    {
        model.SetSetting(key, val);
    }
}
