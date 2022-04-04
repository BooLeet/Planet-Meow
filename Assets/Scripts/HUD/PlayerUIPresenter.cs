using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIPresenter : MonoBehaviour
{
    public Player model;
    [Header("Health")]
    public HealthUI health;

    void Start()
    {
        model.damageable.OnHealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        model.damageable.OnHealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        health.SetValue(model.damageable.currentHealth / model.damageable.maxHealth);
    }
}
