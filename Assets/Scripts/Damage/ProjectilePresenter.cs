using UnityEngine;

public class ProjectilePresenter : MonoBehaviour
{
    public Projectile model;
    public Poolable hitEffectPrefab;

    private void Awake()
    {
        model.OnCollision += StartHitEffect;
        model.OnLifeOver += StartHitEffect;
    }

    private void OnDestroy()
    {
        model.OnCollision -= StartHitEffect;
        model.OnLifeOver -= StartHitEffect;
    }

    private void StartHitEffect()
    {
        Poolable deathEffect = ObjectSpawner.SpawnObject(hitEffectPrefab);
        deathEffect.transform.position = transform.position;
    }
}
