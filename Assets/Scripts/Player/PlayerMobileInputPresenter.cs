using UnityEngine;
using UnityEngine.UI;

public class PlayerMobileInputPresenter : MonoBehaviour
{
    public PlayerMobileInput model;
    public RectTransform joystickBase;
    public RectTransform joystickHandle;
    [Space]
    public CanvasGroup canvasGroup;
    [Space]
    public Button pauseButton;

    void Start()
    {
        model.OnMoveBegin += OnMoveBegin;
        model.OnMove += OnMove;
        model.OnAnchorChanged += OnAnchorChanged;
        model.OnMoveEnd += OnMoveEnd;
        model.OnEnableChanged += UpdateCanvasGroup;

        pauseButton.onClick.AddListener(model.InvokePause);

        joystickBase.gameObject.SetActive(false);
        UpdateCanvasGroup();
    }

    private void OnDestroy()
    {
        model.OnMoveBegin -= OnMoveBegin;
        model.OnAnchorChanged -= OnAnchorChanged;
        model.OnMove -= OnMove;
        model.OnMoveEnd -= OnMoveEnd;
        model.OnEnableChanged -= UpdateCanvasGroup;

        pauseButton.onClick.RemoveListener(model.InvokePause);
    }

    private void OnAnchorChanged()
    {
        joystickBase.position = model.anchorPoint;
    }

    private void OnMove()
    {
        joystickHandle.position = model.movePoint;
    }

    private void OnMoveBegin()
    {
        joystickBase.gameObject.SetActive(true);
    }

    private void OnMoveEnd()
    {
        joystickBase.gameObject.SetActive(false);
    }

    private void UpdateCanvasGroup()
    {
        canvasGroup.alpha = model.isEnabled ? 1 : 0;
        canvasGroup.interactable = model.isEnabled;
    }

}
