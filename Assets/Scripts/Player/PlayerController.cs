using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerInput
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject[] inputObjects;

    [SerializeField]
    private PlayerMobileInput mobileInput;

    [SerializeField]
    private PlayerGamepadInput gamepadInput;

    public event Action OnPause;
    public event Action OnEnableChanged;

    public bool isEnabled
    {
        get;
        private set;
    }

    private List<IPlayerInput> playerInputs = new List<IPlayerInput>();

    protected void Update()
    {
        if (!isEnabled)
        {
            player.Move(Vector2.zero);
            return;
        }


        player.Move(GetMoveInput());
        if (GetAutoAim())
        {
            player.AutoAim();
        }
        else
        {
            player.ManualAim(GetLookInput());
        }

        if (GetAttack())
        {
            player.Attack();
        }
    }

    public void Init()
    {
        playerInputs.Clear();
        foreach (GameObject inputObj in inputObjects)
        {
            if (inputObj.TryGetComponent(out IPlayerInput input))
            {
                playerInputs.Add(input);
                input.OnPause += InvokePause;
            }
        }
    }

    void OnDestroy()
    {
        foreach (IPlayerInput input in playerInputs)
        {
            input.OnPause -= InvokePause;
        }
    }

    public bool GetAttack()
    {
        bool attack = false;
        foreach (IPlayerInput input in playerInputs)
        {
            attack |= input.GetAttack();
        }

        return attack;
    }

    public bool GetAutoAim()
    {
        return false;

        bool autoAim = false;
        foreach (IPlayerInput input in playerInputs)
        {
            autoAim |= input.GetAutoAim();
        }

        return autoAim;
    }

    public Vector2 GetLookInput()
    {
        Vector2 look = Vector2.zero;
        foreach (IPlayerInput input in playerInputs)
        {
            look += input.GetLookInput();
        }

        return look;
    }

    public Vector2 GetMoveInput()
    {
        Vector2 move = Vector2.zero;
        foreach (IPlayerInput input in playerInputs)
        {
            move += input.GetMoveInput();
        }

        return move;
    }

    public void SetEnable(bool val)
    {
        if (isEnabled == val)
        {
            return;
        }

        foreach (IPlayerInput input in playerInputs)
        {
            input.SetEnable(val);
        }

        isEnabled = val;
        OnEnableChanged?.Invoke();
    }

    private void InvokePause()
    {
        OnPause?.Invoke();
    }
}
