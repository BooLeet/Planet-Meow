using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    public Enemy model;
    public Poolable deathEffectPrefab;
    public Transform deathEffectSource;

    void Start()
    {
        model.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        model.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        Poolable deathEffect = ObjectSpawner.SpawnObject(deathEffectPrefab);
        deathEffect.transform.position = deathEffectSource.position;
    }
}
