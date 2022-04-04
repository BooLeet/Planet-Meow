using UnityEngine;
using UnityEngine.Events;

public class ParticleBurstEffect : MonoBehaviour
{
    public float duration = 1;
    public int particleCount = 30;
    public ParticleSystem particles;
    public UnityEvent OnEffectEnd;

    public void StartEffect()
    {
        particles.Emit(30);
        SmoothInterpolator.StartInterpolation(gameObject, duration, null, StopEffect);
    }

    private void StopEffect()
    {
        OnEffectEnd?.Invoke();
    }
}
