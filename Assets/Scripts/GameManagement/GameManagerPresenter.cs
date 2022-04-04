using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPresenter : MonoBehaviour
{
    public GameManager model;

    public PlayerCameraPositioner cameraPositioner;

    void Start()
    {
        model.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        model.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        cameraPositioner.FocusOnPlayer();
    }
}
