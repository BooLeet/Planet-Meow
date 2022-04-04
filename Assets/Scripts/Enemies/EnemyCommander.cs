using System.Collections;
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
    } = 20;

    void Start()
    {
        registry = EnemyRegistry.GetInstance();
    }

    void Update()
    {
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
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = enemyPool.Depool().GetComponent<Enemy>();
        float lat = -player.movement.currentCoordinates.x;
        float lon = SphericalMovement.TrimAngleRad(player.movement.currentCoordinates.y + Mathf.PI);
        Vector2 spawnCoordinates = SphericalMovement.GetDestination(new Vector2(lat, lon), Random.value * 2 * Mathf.PI, Random.value * Mathf.PI / 2);
        enemy.movement.SetLat(spawnCoordinates.x);
        enemy.movement.SetLon(spawnCoordinates.y);
    }

    public void SetTargetEnemyCount(int val)
    {
        targetEnemyCount = val;
    }
}
