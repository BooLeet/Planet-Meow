using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public EnemyCommander enemyCommander;
    public GameDifficulty gameDifficulty;
    public GameRecords gameRecords
    {
        get;
        private set;
    }

    public bool isPlaying
    { 
        get;
        private set;
    }

    public bool isPaused
    {
        get;
        private set;
    }

    public float playTime
    {
        get;
        private set;
    }

    public int killCount
    {
        get;
        private set;
    }

    public event Action OnPlayerDeath;
    public event Action OnPlayStateChange;
    public event Action OnPauseChange;
    public event Action OnPlayTimeChanged;
    public event Action OnKillCountChanged;

    void Start()
    {
        gameRecords = GameRecords.LoadRecords();

        player.OnDeath += HandlePlayerDeath;
        player.damageable.OnDamageTaken += OnPlayerDamaged;
        Enemy.OnDeathStatic += OnEnemyDeath;
    }

    private void OnDestroy()
    {
        if (player)
        {
            player.OnDeath -= HandlePlayerDeath;
            player.damageable.OnDamageTaken -= OnPlayerDamaged;
        }
        Enemy.OnDeathStatic -= OnEnemyDeath;
    }

    private void Update()
    {
        if (!isPlaying || isPaused)
        {
            return;
        }

        SetPlayTime(playTime + Time.deltaTime);
        enemyCommander.SetTargetEnemyCount(gameDifficulty.GetEnemyCount(playTime));
    }

    private void HandlePlayerDeath()
    {
        isPlaying = false;
        enemyCommander.Disable();
        enemyCommander.KillAll();

        gameRecords.UpdateRecords(new GameRecords.Record(killCount, playTime));

        OnPlayerDeath?.Invoke();
        OnPlayStateChange?.Invoke();
    }

    public void StartGame()
    {
        if (isPlaying)
        {
            Debug.LogError("Game is already playing");
            return;
        }
        player.damageable.Revive();
        SetPlayTime(0);
        SetKillCount(0);

        enemyCommander.Enable();
        enemyCommander.SetTargetEnemyCount(0);
        isPlaying = true;
        OnPlayStateChange?.Invoke();
    }

    public void PauseGame()
    {
        SetPauseValue(true);
    }

    public void UnpauseGame()
    {
        SetPauseValue(false);
    }

    public void SetPauseValue(bool val)
    {
        if (isPaused == val)
        {
            return;
        }
        isPaused = val;
        enemyCommander.StopEnemies();
        enemyCommander.SetEnable(!isPaused);
        OnPauseChange?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetPlayTime(float val)
    {
        playTime = val;
        OnPlayTimeChanged?.Invoke();
    }

    private void SetKillCount(int val)
    {
        killCount = val;
        OnKillCountChanged?.Invoke();
    }

    private void OnEnemyDeath(Enemy obj)
    {
        if (isPlaying)
        {
            SetKillCount(killCount + 1);
        }
    }

    private void OnPlayerDamaged()
    {
        if (isPlaying)
        {
            SetKillCount(killCount - 1);
        }
    }
}
