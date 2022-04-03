using System;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    private ObjectPool pool;

    public event Action OnDepooled;
    public event Action OnEnpooled;

    public void Depool(ObjectPool pool)
    {
        this.pool = pool;
        OnDepooled?.Invoke();
        gameObject.SetActive(true);
    }

    public void Enpool()
    {
        gameObject.SetActive(false);
        pool.Enpool(this);
        OnEnpooled?.Invoke();
    }
}
