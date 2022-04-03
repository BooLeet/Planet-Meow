using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth
    {
        get;
        private set;
    }

    public bool isDead
    {
        get;
        private set;
    }

    public event Action OnHealthChanged;
    public event Action OnDamageTaken;
    public event Action OnDeath;
    public event Action OnRevive;

    public void Start()
    {
        SetHealth(maxHealth);
    }

    private void SetHealth(float val)
    {
        currentHealth = val;
        OnHealthChanged?.Invoke();
    }

    public void Revive()
    {
        SetHealth(maxHealth);
        isDead = false;
        OnRevive?.Invoke();
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
        {
            return;
        }

        float newHealth = currentHealth - amount;
        if (newHealth <= 0)
        {
            newHealth = 0;
            isDead = true;
            OnDeath?.Invoke();
        }
        SetHealth(newHealth);
    }
}
