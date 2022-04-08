using UnityEngine;

public abstract class GameDifficulty : ScriptableObject
{
    public abstract int GetEnemyCount(float time);
}
