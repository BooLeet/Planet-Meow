using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGamepadInput : MonoBehaviour, IPlayerInput
{
    [SerializeField]
    private InputActionReference moveInput;

    [SerializeField]
    private InputActionReference lookInput;

    [SerializeField]
    private InputActionReference pauseInput;

    public bool isEnabled
    {
        get;
        private set;
    }


    public event Action OnPause;
    public event Action OnEnableChanged;

    private bool wasPausePressed;

    void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        bool isPausePressed = pauseInput.action.ReadValue<float>() > 0;
        if (isPausePressed && !wasPausePressed)
        {
            OnPause?.Invoke();
        }

        wasPausePressed = isPausePressed;
    }

    public bool GetAttack()
    {
        return GetLookInput().magnitude > 0;
    }

    public bool GetAutoAim()
    {
        return false;
    }

    public Vector2 GetLookInput()
    {
        Vector2 input = lookInput.action.ReadValue<Vector2>();
        if (input.magnitude > 0)
        {
            input = input.normalized;
        }

        return input;
    }

    public Vector2 GetMoveInput()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        if (input.magnitude > 0)
        {
            input = input.normalized;
        }

        return input;
    }

    public void SetEnable(bool val)
    {
        if (isEnabled == val)
        {
            return;
        }

        isEnabled = val;
        if (val)
        {
            moveInput.action.Enable();
            lookInput.action.Enable();
            pauseInput.action.Enable();
        }
        else
        {
            moveInput.action.Disable();
            lookInput.action.Disable();
            pauseInput.action.Disable();

            wasPausePressed = false;
        }

        OnEnableChanged?.Invoke();
    }
}
