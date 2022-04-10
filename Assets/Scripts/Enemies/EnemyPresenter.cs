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
    [Space]
    public Animator animator;
    public string spawnTrigger = "Spawn";
    public string despawnTrigger = "Despawn";

    void Start()
    {
        model.OnSpawn += OnSpawn;
        model.OnDeath += OnDeath;
        model.OnDespawn += OnDespawn;
    }

    private void OnDestroy()
    {
        model.OnSpawn -= OnSpawn;
        model.OnDeath -= OnDeath;
        model.OnDespawn -= OnDespawn;
    }

    private void OnSpawn()
    {
        animator.SetTrigger(spawnTrigger);
    }

    private void OnDeath()
    {
        ObjectSpawner.SpawnObject(deathEffectPrefab, deathEffectSource.position);
        
        ObjectSpawner.SpawnObject(deathSoundPrefab, deathEffectSource.position);
    }

    private void OnDespawn()
    {
        animator.SetTrigger(despawnTrigger);
    }
}
