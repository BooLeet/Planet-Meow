using System.Collections.Generic;
using UnityEngine;

public class EnemyRegistry : MonoBehaviour
{
    private static EnemyRegistry instance;

    private HashSet<Enemy> enemies = new HashSet<Enemy>();

    public static EnemyRegistry GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("[EnemyRegistry]");
            instance = obj.AddComponent<EnemyRegistry>();
        }

        return instance;
    }

    public static void Register(Enemy enemy)
    {
        GetInstance().enemies.Add(enemy);
    }

    public static void Unregister(Enemy enemy)
    {
        if (instance == null)
        {
            return;
        }
        instance.enemies.Remove(enemy);
    }

    public static HashSet<Enemy> GetEnemies()
    {
        if (instance == null)
        {
            return null;
        }

        return instance.enemies;
    }
}
