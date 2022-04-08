using System;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    public Player player;

    public event Action OnPause;

    public bool isEnabled
    {
        get;
        private set;
    }

    public event Action OnEnableChanged;

    protected void Update()
    {
        if (!isEnabled)
        {
            player.Move(Vector2.zero);
            return;
        }


        player.Move(GetMoveInput());

        if (GetAttack())
        {
            player.Attack();
        }
    }

    protected abstract Vector2 GetMoveInput();

    protected abstract bool GetAttack();

    public void InvokePause()
    {
        OnPause?.Invoke();
    }

    public void SetEnable(bool val)
    {
        if (isEnabled != val)
        {
            isEnabled = val;
        }
        OnEnableChanged?.Invoke();
    }

    public void Enable()
    {
        SetEnable(true);
    }

    public void Disable()
    {
        SetEnable(false);
    }
}
