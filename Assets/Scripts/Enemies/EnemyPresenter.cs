using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    public Enemy model;
    public Poolable deathEffectPrefab;
    public Poolable deathSoundPrefab;
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
        ObjectSpawner.SpawnObject(deathEffectPrefab, deathEffectSource.position);
        
        ObjectSpawner.SpawnObject(deathSoundPrefab, deathEffectSource.position);
    }
}
