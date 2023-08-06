using System;
using UnityEngine;

public class PlayerMobileInput : MonoBehaviour, IPlayerInput
{
    public MobileDragArea mobileDragArea;
    public Canvas canvas;

    public float defaultScreenWidth = 1080;
    public float joystickRadius = 100;

    public bool isEnabled
    {
        get;
        private set;
    }

    public Vector2 movePoint
    {
        get;
        private set;
    }

    public Vector2 anchorPoint
    {
        get;
        private set;
    }

    private bool isDragging;

    public event Action OnMoveBegin;
    public event Action OnMove;
    public event Action OnMoveEnd;
    public event Action OnAnchorChanged;

    public event Action OnPause;
    public event Action OnEnableChanged;

    private void Start()
    {
        mobileDragArea.OnDragBegin += OnDragBegin;
        mobileDragArea.OnDrag += OnDrag;
        mobileDragArea.OnDragEnd += OnDragEnd;
    }

    private void OnDestroy()
    {
        mobileDragArea.OnDragBegin -= OnDragBegin;
        mobileDragArea.OnDrag -= OnDrag;
        mobileDragArea.OnDragEnd -= OnDragEnd;
    }

    private void OnDrag()
    {
        movePoint += mobileDragArea.dragInput;
        Vector2 input = movePoint - anchorPoint;
        float correctedJoystickRadius = joystickRadius * canvas.pixelRect.width / defaultScreenWidth;
        if (input.magnitude > correctedJoystickRadius)
        {
            anchorPoint += input.normalized * (input.magnitude - correctedJoystickRadius);
            OnAnchorChanged?.Invoke();
        }
        OnMove?.Invoke();
    }

    private void OnDragBegin()
    {
        isDragging = true;
        anchorPoint = mobileDragArea.dragBeginPosition;
        movePoint = anchorPoint;
        OnAnchorChanged?.Invoke();
        OnMoveBegin?.Invoke();
    }

    private void OnDragEnd()
    {
        isDragging = false;
        anchorPoint = movePoint = Vector2.zero;
        OnMoveEnd?.Invoke();
        OnAnchorChanged?.Invoke();
    }

    public Vector2 GetMoveInput()
    {
        Vector2 input = movePoint - anchorPoint;
        if (input.magnitude == 0)
        {
            return Vector2.zero;
        }

        return input.normalized;
    }

    public bool GetAttack()
    {
        return GetMoveInput().magnitude > 0 && EnemyRegistry.GetEnemies() != null && EnemyRegistry.GetEnemies().Count > 0;
    }

    public bool GetAutoAim()
    {
        return isDragging;
    }

    public Vector2 GetLookInput()
    {
        return Vector2.zero;
    }

    public void SetEnable(bool val)
    {
        if (isEnabled == val)
        {
            return;
        }

        isEnabled = val;
        OnEnableChanged?.Invoke();
    }

    public void InvokePause()
    {
        OnPause?.Invoke();
    }
}
