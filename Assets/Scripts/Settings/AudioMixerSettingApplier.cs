using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Settings/Applier/AudioMixer")]
public class AudioMixerSettingApplier : SettingApplier
{
    public AudioMixer audioMixer;
    public string mixerFloatKey;
    public string onKey = "on";
    public string offKey = "off";
    public int maxSettingValue = 10;
    public float maxVolume = 0.5f;

    public override void ApplySetting(string value)
    {
        float val = int.Parse(value);
        float volume = maxVolume * (float)(val / maxSettingValue);
        audioMixer.SetFloat(mixerFloatKey, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20);
    }
}
