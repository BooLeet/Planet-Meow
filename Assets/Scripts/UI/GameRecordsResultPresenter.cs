using UnityEngine;

public class GameRecordsResultPresenter : MonoBehaviour
{
    public BubbleText bubbleText;
    public string pointsKey = "Points";
    public string newRecordKey = "New Record!";
    public string recordKey = "Record";

    public void ShowRecords(int currentPoints ,GameRecords records)
    {
        string text = $"{Localizer.Localize(pointsKey)}: {currentPoints}";

        text += "\n" + (records.newPointsRecord ? Localizer.Localize(newRecordKey) : "");

        text += $"\n{Localizer.Localize(recordKey)}: {records.points}";

        bubbleText.SetText(text);
    }
}
