using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    public Text text;
    public Button button;
    private SettingsManagerPresenter presenter;
    private SettingsManager.Setting setting;
    private string currentValue;

    public void Setup(SettingsManagerPresenter presenter, SettingsManager.Setting setting)
    {
        this.presenter = presenter;
        this.setting = setting;
        button.onClick.AddListener(CycleSetting);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(CycleSetting);
    }

    private void CycleSetting()
    {
        // currentValue = ...
        presenter.SetValue(setting.key, currentValue);
    }

    public void SetSetting(string val)
    {
        currentValue = val;
        UpdateText(setting.key, val);
    }

    public void UpdateText(string key, string val)
    {
        text.text = $"{Localizer.Localize(key)}: {Localizer.Localize(val)}";
    }
}
