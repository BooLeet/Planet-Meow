using UnityEngine;

[CreateAssetMenu(menuName = "Game/Difficulty/Linear")]
public class GameDifficultyLinear : GameDifficulty
{
    public float growthTime = 120f;
    public int minValue;
    public int maxValue;

    public override int GetEnemyCount(float time)
    {
        if (time < growthTime)
        {
            return minValue + (int)((maxValue - minValue) * time / growthTime);
        }
        else
        {
            return maxValue;
        }
    }
}
