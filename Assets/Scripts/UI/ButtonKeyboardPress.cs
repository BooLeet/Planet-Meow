using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonKeyboardPress : MonoBehaviour
{
    private Button button;
    private EventTrigger eventTrigger;

    public KeyCode keyCode;

    public UnityEvent OnKeyDown;
    public UnityEvent OnKeyUp;

    private void Start()
    {
        button = GetComponent<Button>();
        eventTrigger = GetComponent<EventTrigger>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            OnKeyDown?.Invoke();
            eventTrigger.OnPointerDown(new PointerEventData(EventSystem.current));
        }
        if (Input.GetKeyUp(keyCode))
        {
            OnKeyUp?.Invoke();
            eventTrigger.OnPointerUp(new PointerEventData(EventSystem.current));
            eventTrigger.OnPointerClick(new PointerEventData(EventSystem.current));
            button.onClick.Invoke();
        }
    }
}
