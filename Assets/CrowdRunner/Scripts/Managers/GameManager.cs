using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Action<GameState> onGameStateChanged;

    private GameState gameState = GameState.None;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);

        Debug.Log("Game state is " + gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
