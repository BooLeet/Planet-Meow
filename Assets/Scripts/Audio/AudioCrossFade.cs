using UnityEngine;

public class AudioCrossFade : MonoBehaviour
{
    public AudioSource firstSource;
    public AudioSource secondSource;

    [Range(0,1)]
    public float fadeParameter = 0;

    private void Start()
    {
        UpdateVolumes();
        firstSource.Play();
        secondSource.Play();
    }

    void Update()
    {
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        firstSource.volume = 1 - fadeParameter;
        secondSource.volume = fadeParameter;
    }

    public void ChangeFadeParameter(float value)
    {
        fadeParameter = Mathf.Clamp01(value);
    }
}
