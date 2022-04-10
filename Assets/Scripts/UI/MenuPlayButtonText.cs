using UnityEngine.UI;

public class MenuPlayButtonText : BaseObjectLocalizer
{
    public Text text; 
    public string playKey = "Play";
    public string separator = "|";
    public string recordKey = "Record";

    public int noRecordFontSize = 116;
    public int recordFontSize = 77;

    private int record;

    public void UpdateRecord(int record)
    {
        this.record = record;
        UpdateText();
    }

    protected override void UpdateText()
    {
        string str = Localizer.Localize(playKey);
        if (record != 0)
        {
            str += $"{separator}{Localizer.Localize(recordKey)}: {record}";
        }
        text.text = str;
        text.fontSize = record > 0 ? recordFontSize : noRecordFontSize;
    }
}
