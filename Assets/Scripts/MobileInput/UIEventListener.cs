using UnityEngine;
using UnityEngine.EventSystems;


public abstract class UIEventListener : MonoBehaviour
{
    public EventTrigger eventTrigger;
    protected void AddListener(EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<PointerEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;

        entry.callback.AddListener((data) => callback((PointerEventData)data));
        eventTrigger.triggers.Add(entry);
    }
}
