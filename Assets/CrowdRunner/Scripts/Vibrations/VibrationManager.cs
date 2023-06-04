using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private bool isVibration = true;

    private void OnEnable()
    {
        PlayerCollide.onDoorsHit -= Vibrate;
        Enemy.onRunnerDied -= Vibrate;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void OnDisable()
    {
        PlayerCollide.onDoorsHit -= Vibrate;
        Enemy.onRunnerDied -= Vibrate;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        if(gameState == GameState.LevelComplete || gameState == GameState.GameOver)
            Vibrate();
    }

    private void Vibrate()
    {
        if(isVibration)
            Taptic.Light();
    }

    public void DisableVibrations()
    {
        isVibration = false;
    }

    public void EnableVibrations()
    {
        isVibration = true;
    }
}
