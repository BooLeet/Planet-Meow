using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Applier/FPS Counter")]
public class FPSCounterSettingApplier : SettingApplier
{
    public string onValue = "on";

    public override void ApplySetting(string value)
    {
        FPSCounter counter = FPSCounter.instance;
        if (counter == null)
        {
            return;
        }

        counter.show = value == onValue;
    }
}
