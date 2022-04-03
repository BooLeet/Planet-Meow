using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Poolable poolable;

    private Queue<Poolable> pool = new Queue<Poolable>();

    public Poolable Depool()
    {
        Poolable result;
        if (pool.Count > 0)
        {
            result = pool.Dequeue();
            result.Depool(this);

            return result;
        }

        result = Instantiate(poolable);
        result.Depool(this);
        return result;
    }

    public void Enpool(Poolable poolable)
    {
        pool.Enqueue(poolable);
    }
}
