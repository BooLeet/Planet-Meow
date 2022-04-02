using UnityEngine;

public class PlayerMobileInputPresenter : MonoBehaviour
{
    public PlayerMobileInput model;
    public RectTransform joystickBase;
    public RectTransform joystickHandle;

    void Start()
    {
        model.OnMoveBegin += OnMoveBegin;
        model.OnMove += OnMove;
        model.OnAnchorChanged += OnAnchorChanged;
        model.OnMoveEnd += OnMoveEnd;

        joystickBase.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        model.OnMoveBegin -= OnMoveBegin;
        model.OnAnchorChanged -= OnAnchorChanged;
        model.OnMove -= OnMove;
        model.OnMoveEnd -= OnMoveEnd;
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
}
