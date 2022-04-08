using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    public Enemy model;
    public Transform deathEffectSource;
    [Space]
    public Poolable deathEffectPrefab;
    public Poolable deathSoundPrefab;
    public Poolable teleportEffectPrefab;

    void Start()
    {
        model.OnDeath += OnDeath;
        model.OnDespawn += OnDespawn;
    }

    private void OnDestroy()
    {
        model.OnDeath -= OnDeath;
        model.OnDespawn -= OnDespawn;
    }

    private void OnDeath()
    {
        ObjectSpawner.SpawnObject(deathEffectPrefab, deathEffectSource.position);
        
        ObjectSpawner.SpawnObject(deathSoundPrefab, deathEffectSource.position);
    }

    private void OnDespawn()
    {
        ObjectSpawner.SpawnObject(teleportEffectPrefab, deathEffectSource.position);
    }
}
