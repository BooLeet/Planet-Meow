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
        int currentIndex = -1;
        for (int i = 0; i < setting.values.Length; ++i)
        {
            if (setting.values[i] == currentValue)
            {
                currentIndex = i;
                break;
            }
        }

        currentIndex = (currentIndex + 1) % setting.values.Length;

        SetSetting(setting.values[currentIndex]);
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
