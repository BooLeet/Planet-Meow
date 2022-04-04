using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private static ObjectSpawner instance;

    private Dictionary<Poolable, ObjectPool> pools = new Dictionary<Poolable, ObjectPool>();

    public static ObjectSpawner GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("[ObjectSpawner]");
            instance = obj.AddComponent<ObjectSpawner>();
        }

        return instance;
    }

    public static Poolable SpawnObject(Poolable prefab)
    {
        return GetInstance()._SpawnObject(prefab);
    }

    public static Poolable SpawnObject(Poolable prefab, Vector3 position)
    {
        Poolable poolable = GetInstance()._SpawnObject(prefab);
        poolable.transform.position = position;
        return poolable;
    }

    private Poolable _SpawnObject(Poolable prefab)
    {
        ObjectPool pool = null;
        if (!pools.TryGetValue(prefab, out pool))
        {
            GameObject obj = new GameObject($"{prefab.name} Pool");
            obj.transform.parent = transform;
            pool = obj.AddComponent<ObjectPool>();
            pools.Add(prefab, pool);
        }

        pool.poolable = prefab;
        return pool.Depool();
    }
}
