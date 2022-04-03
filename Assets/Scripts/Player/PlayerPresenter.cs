using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    public Player model;
    public Animator animator;
    public string[] attackTriggers;
    private int attackTriggerIndex;

    void Start()
    {
        model.OnAttack += OnAttack;
    }

    private void OnDestroy()
    {
        model.OnAttack -= OnAttack;
    }

    private void OnAttack()
    {
        animator.SetTrigger(attackTriggers[attackTriggerIndex]);
        attackTriggerIndex = (attackTriggerIndex + 1) % attackTriggers.Length;
    }
}
