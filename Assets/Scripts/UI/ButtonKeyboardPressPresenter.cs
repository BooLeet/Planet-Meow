using UnityEngine;
using UnityEngine.UI;

public class ButtonKeyboardPressPresenter : BaseObjectLocalizer
{
    private ButtonKeyboardPress keyboardPress;
    public Text text;
    public GameObject graphicContainer;

    protected override void Start()
    {
        keyboardPress = GetComponentInParent<ButtonKeyboardPress>();
        graphicContainer.SetActive(keyboardPress != null);
        base.Start();
    }

    protected override void UpdateText()
    {
        if (keyboardPress != null)
        {
            text.text = $"[{Localizer.Localize(keyboardPress.keyCode.ToString())}]";
        }
    }
}
