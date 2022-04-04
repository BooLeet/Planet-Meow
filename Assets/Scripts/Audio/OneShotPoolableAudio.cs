using UnityEngine;
using UnityEngine.Events;

public class OneShotPoolableAudio : MonoBehaviour
{
    public AudioSource source;

    public UnityEvent OnEffectEnd;

    public void PlayAudio()
    {
        source.Play();
        SmoothInterpolator.StartInterpolation(gameObject, source.clip.length, null, OnPlayComplete);
    }

    private void OnPlayComplete()
    {
        source.Stop();
        OnEffectEnd?.Invoke();
    }
}
