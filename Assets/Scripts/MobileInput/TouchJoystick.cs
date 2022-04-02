using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchJoystick : MobileDragArea
{
    public RectTransform joyTransform;
    public RectTransform handleTransform;

    public bool hideHandleWhenIncative = true;

    public bool press
    {
        get;
        private set;
    }

    public bool pressDown
    {
        get;
        private set;
    }

    public bool pressUp
    {
        get;
        private set;
    }

    public Vector2 joyInput
    {
        get;
        private set;
    }

    public event Action OnInputChanged;

    protected override void Start()
    {
        base.Start();

        AddListener(EventTriggerType.Drag, MoveDelegate);
        AddListener(EventTriggerType.PointerDown, MoveDelegate);

        AddListener(EventTriggerType.EndDrag, ResetDelegate);
        AddListener(EventTriggerType.PointerUp, ResetDelegate);


        SetHandleActive(false);
    }

    private void LateUpdate()
    {
        pressDown = false;
        pressUp = false;
    }

    private void MoveDelegate(PointerEventData data)
    {
        Vector2 position = new Vector3(data.position.x, data.position.y) - joyTransform.position;
        position.x /= joyTransform.rect.width / 2;
        position.y /= joyTransform.rect.height / 2;

        if (position.magnitude > 1)
        {
            position.Normalize();
        }

        joyInput = position;
        press = true;
        pressDown = true;
        OnInputChanged?.Invoke();
        UpdateHandlePosition();
        SetHandleActive(true);
    }

    private void ResetDelegate(PointerEventData data)
    {
        joyInput = Vector2.zero;
        press = false;
        pressUp = true;
        OnInputChanged?.Invoke();
        UpdateHandlePosition();
        SetHandleActive(false);
    }

    private void UpdateHandlePosition()
    {
        Vector2 handlePosition = new Vector2(joyInput.x * joyTransform.rect.width / 2, joyInput.y * joyTransform.rect.height / 2);
        handleTransform.localPosition = handlePosition;
    }

    private void OnDisable()
    {
        joyInput = Vector2.zero;
    }

    private void SetHandleActive(bool value)
    {
        if (hideHandleWhenIncative)
        {
            handleTransform.gameObject.SetActive(value);
        }
    }

}
