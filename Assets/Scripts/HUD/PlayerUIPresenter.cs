using UnityEngine;

public class PlayerUIPresenter : MonoBehaviour
{
    public Player model;
    public CanvasGroup canvasGroup;

    [Header("Health")]
    public HealthUI health;

    void Start()
    {
        model.damageable.OnHealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        if (model != null)
        {
            model.damageable.OnHealthChanged -= OnHealthChanged;
        }
    }
    private void OnHealthChanged()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        health.SetValue(model.damageable.currentHealth / model.damageable.maxHealth);
    }

    public void Hide()
    {
        ShowHide(false);
    }

    public void Show()
    {
        ShowHide(true);
    }

    public void ShowHide(bool show)
    {
        canvasGroup.alpha = show == true? 1 : 0;
        canvasGroup.interactable = show;
    }
}
