using System;
using UnityEngine;

public interface IPlayerInput
{
    public event Action OnPause;
    public event Action OnEnableChanged;

    public bool isEnabled
    {
        get;
    }

    public Vector2 GetMoveInput();

    public bool GetAttack();

    public bool GetAutoAim();

    public Vector2 GetLookInput();

    public void SetEnable(bool val);
}
