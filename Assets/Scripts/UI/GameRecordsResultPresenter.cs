public class GameRecordsResultPresenter : BaseObjectLocalizer
{
    public BubbleText bubbleText;
    public string pointsKey = "Points";
    public string newRecordKey = "New Record!";
    public string recordKey = "Record";

    private int currentPoints;
    private int points;
    private bool newPointsRecord;

    public void ShowRecords(int currentPoints, GameRecords records)
    {
        this.currentPoints = currentPoints;
        points = records.points;
        newPointsRecord = records.newPointsRecord;
        UpdateText();
    }

    protected override void UpdateText()
    {
        string text = $"{Localizer.Localize(pointsKey)}: {currentPoints}";

        text += "\n" + (newPointsRecord ? Localizer.Localize(newRecordKey) : "");

        text += $"\n{Localizer.Localize(recordKey)}: {points}";

        bubbleText.SetText(text);
    }
}
