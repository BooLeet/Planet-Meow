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

    public bool lateDepoolEventInvocation;

    public void Depool(ObjectPool pool)
    {
        this.pool = pool;
        if (!lateDepoolEventInvocation)
        {
            OnDepooled?.Invoke();
            OnDepooledEvent?.Invoke();
        }
        
        gameObject.SetActive(true);

        if (lateDepoolEventInvocation)
        {
            OnDepooled?.Invoke();
            OnDepooledEvent?.Invoke();
        }
    }

    public void Enpool()
    {
        gameObject.SetActive(false);
        pool.Enpool(this);
        OnEnpooled?.Invoke();
        OnEnpooledEvent?.Invoke();
    }
}
