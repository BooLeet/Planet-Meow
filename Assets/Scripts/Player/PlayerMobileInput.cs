using System;
using UnityEngine;

public class PlayerMobileInput : PlayerInput
{
    public MobileDragArea mobileDragArea;
    public Canvas canvas;

    public float defaultScreenWidth = 1080;
    public float joystickRadius = 100;

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

    public event Action OnMoveBegin;
    public event Action OnMove;
    public event Action OnMoveEnd;
    public event Action OnAnchorChanged;

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
        anchorPoint = mobileDragArea.dragBeginPosition;
        movePoint = anchorPoint;
        OnAnchorChanged?.Invoke();
        OnMoveBegin?.Invoke();
    }

    private void OnDragEnd()
    {
        anchorPoint = movePoint = Vector2.zero;
        OnMoveEnd?.Invoke();
        OnAnchorChanged?.Invoke();
    }

    protected override Vector2 GetMoveInput()
    {
        Vector2 input = movePoint - anchorPoint;
        if (input.magnitude == 0)
        {
            return Vector2.zero;
        }

        return input.normalized;
    }

    protected override bool GetAttack()
    {
        return GetMoveInput().magnitude > 0;
    }
}
