using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Applier/Language")]
public class LanguageSettingApplier : SettingApplier
{
    public override void ApplySetting(string value)
    {
        Localizer localizer = Localizer.instance;
        if (localizer == null)
        {
            return;
        }

        foreach (LocalizationContainer container in localizer.localizationContainers)
        {
            if (container.languageName == value)
            {
                localizer.currentContainer = container;
                return;
            }
        }
    }
}
