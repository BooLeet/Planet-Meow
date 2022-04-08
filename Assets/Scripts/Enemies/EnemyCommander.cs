using System.Collections.Generic;
using UnityEngine;

public class EnemyCommander : MonoBehaviour
{
    public ObjectPool enemyPool;
    public Player player;

    private EnemyRegistry registry;

    public int targetEnemyCount
    {
        get;
        private set;
    } = 0;

    public bool isEnabled
    {
        get;
        private set;
    }

    void Start()
    {
        registry = EnemyRegistry.GetInstance();
    }

    private void OnDestroy()
    {

    }

    void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        HashSet<Enemy> enemies = EnemyRegistry.GetEnemies();
        if (enemies == null)
        {
            return;
        }

        int enemiesToSpawn = targetEnemyCount - enemies.Count;
        for (int i = 0; i < enemiesToSpawn; ++i)
        {
            SpawnEnemy();
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.UpdateTargetPosition(player.movement.currentCoordinates);
            enemy.UpdateDespawnTimer(Time.deltaTime);
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = enemyPool.Depool().GetComponent<Enemy>();
        float lat = -player.movement.currentCoordinates.x;
        float lon = SphericalMovement.TrimAngleRad(player.movement.currentCoordinates.y + Mathf.PI);
        float bearing = SphericalMovement.TrimAngleRad(player.movement.currentBearing + Mathf.PI + Random.Range(-1f, 1f) * Mathf.PI / 6f);
        Vector2 spawnCoordinates = SphericalMovement.GetDestination(new Vector2(lat, lon), bearing, Random.value * Mathf.PI / 2);
        enemy.movement.SetLat(spawnCoordinates.x);
        enemy.movement.SetLon(spawnCoordinates.y);
    }

    public void SetTargetEnemyCount(int val)
    {
        targetEnemyCount = val;
    }

    public void KillAll()
    {
        HashSet<Enemy> enemies = EnemyRegistry.GetEnemies();
        if (enemies == null)
        {
            return;
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.damageable.Kill();
        }
    }

    public void StopEnemies()
    {
        HashSet<Enemy> enemies = EnemyRegistry.GetEnemies();
        if (enemies == null)
        {
            return;
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.StopMovement();
        }
    }

    public void SetEnable(bool val)
    {
        if (isEnabled == val)
        {
            return;
        }

        isEnabled = val;
    }

    public void Enable()
    {
        SetEnable(true);
    }

    public void Disable()
    {
        SetEnable(false);
    }
}
