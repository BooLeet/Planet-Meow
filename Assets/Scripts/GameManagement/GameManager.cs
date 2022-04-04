using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    public event Action OnPlayerDeath;

    void Start()
    {
        player.OnDeath += HandlePlayerDeath;
    }

    private void OnDestroy()
    {
        player.OnDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
}
