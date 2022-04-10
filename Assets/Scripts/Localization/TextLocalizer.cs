using UnityEngine.UI;

public class TextLocalizer : BaseObjectLocalizer
{
    private Text text;
    private string localizationKey;

    protected override void UpdateText()
    {
        GetText().text = Localizer.Localize(localizationKey);
    }

    private Text GetText()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
            localizationKey = text.text;
        }

        return text;
    }
}
