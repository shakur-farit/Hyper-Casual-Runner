using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;

    private void OnEnable()
    {
        PlayerCollide.onDoorsHit += PlayDoorHitSound;

        GameManager.onGameStateChanged += GameStateChangedCallback;

        Runner.onRunnerDied += PlayRunnerDieSound;
    }

    private void OnDisable()
    {
        PlayerCollide.onDoorsHit -= PlayDoorHitSound;

        GameManager.onGameStateChanged -= GameStateChangedCallback;

        Runner.onRunnerDied -= PlayRunnerDieSound;
    }

    private void PlayDoorHitSound()
    {
        if (doorHitSound == null) 
        {
            Debug.LogWarning("Hitting of doors have no sound. Add it in Sound Manager!");
            return;
        }

        doorHitSound.Play();
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        if (levelCompleteSound == null)
        {
            Debug.LogWarning("Level completing have no sound. Add it in Sound Manager!");
            return;
        }

        if (gameOverSound == null)
        {
            Debug.LogWarning("Game overing have no sound. Add it in Sound Manager!");
            return;
        }

        if (gameState == GameState.LevelComplete)
            levelCompleteSound.Play();
        else if(gameState == GameState.GameOver)
            gameOverSound.Play();
    }

    private void PlayRunnerDieSound()
    {
        if (runnerDieSound == null)
        {
            Debug.LogWarning("Runner dying have no sound. Add it in Sound Manager!");
            return;
        }

        runnerDieSound.Play();
    }

    public void DisableSounds()
    {
        doorHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameOverSound.volume = 0;
        buttonSound.volume = 0;
    }

    public void EnableSounds()
    {
        doorHitSound.volume = 0.5f;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 0.7f;
        gameOverSound.volume = 0.7f;
        buttonSound.volume = 1;
    }
}
