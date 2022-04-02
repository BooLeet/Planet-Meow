using UnityEngine.EventSystems;

public class MobileButton : UIEventListener
{
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

    void Start()
    {
        AddListener(EventTriggerType.PointerDown, HandlePointerDown);
        AddListener(EventTriggerType.PointerUp, HandlePointerUp);
        AddListener(EventTriggerType.PointerExit, HandlePointerUp);
    }
    private void HandlePointerDown(PointerEventData arg0)
    {
        press = true;
        pressDown = true;
    }

    private void HandlePointerUp(PointerEventData arg0)
    {
        press = false;
        pressUp = true;
    }


    private void LateUpdate()
    {
        pressDown = false;
        pressUp = false;
    }
}
