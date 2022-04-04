using System;
using UnityEngine;
using UnityEngine.Events;

public class Poolable : MonoBehaviour
{
    private ObjectPool pool;

    public event Action OnDepooled;
    public event Action OnEnpooled;

    public UnityEvent OnDepooledEvent;
    public UnityEvent OnEnpooledEvent;

    public void Depool(ObjectPool pool)
    {
        this.pool = pool;
        OnDepooled?.Invoke();
        OnDepooledEvent?.Invoke();
        gameObject.SetActive(true);
    }

    public void Enpool()
    {
        gameObject.SetActive(false);
        pool.Enpool(this);
        OnEnpooled?.Invoke();
        OnEnpooledEvent?.Invoke();
    }
}
