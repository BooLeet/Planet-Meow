using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Settings/Applier/AudioMixer")]
public class AudioMixerSettingApplier : SettingApplier
{
    public AudioMixer audioMixer;
    public string mixerFloatKey;
    public string onKey = "on";
    public string offKey = "off";
    public float onValue = 1;

    public override void ApplySetting(string value)
    {
        float val = value == offKey ? 0 : 1;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(val, 0.0001f, 1)) * 20);
    }
}
