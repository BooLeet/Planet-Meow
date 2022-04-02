using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileDragArea : UIEventListener
{
    public Vector2 dragInput
    {
        get;
        private set;
    }

    public Vector2 dragBeginPosition
    {
        get;
        private set;
    }

    private Vector2 previousDragPosition;

    public event Action OnDragBegin;
    public event Action OnDrag;
    public event Action OnDragEnd;

    protected virtual void Start()
    {
        AddListener(EventTriggerType.BeginDrag, HandleBeginDrag);
        AddListener(EventTriggerType.Drag, HandleDrag);
        AddListener(EventTriggerType.EndDrag, HandleEndDrag);
    }

    private void HandleBeginDrag(PointerEventData data)
    {
        previousDragPosition = data.position;
        dragBeginPosition = data.position;
        OnDragBegin?.Invoke();
    }

    private void HandleDrag(PointerEventData data)
    {
        dragInput = data.position - previousDragPosition;
        previousDragPosition = data.position;
        OnDrag?.Invoke();
    }

    private void HandleEndDrag(PointerEventData data)
    {
        dragInput = Vector2.zero;
        OnDragEnd?.Invoke();
    }
}
