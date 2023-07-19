using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private TMP_Text crowdCounter;


    private void OnEnable()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void Start()
    {
        progressBar.value = 0;

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingsPanel.SetActive(false);

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void RetryButtonPressed()
    {
        LevelManager.instance.RestartLevel();
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState())
            return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }

    public void ShowGameOverPanel()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowLevelCompletePanel()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.GameOver)
            ShowGameOverPanel();
        else if (gameState == GameState.LevelComplete)
        {
            ShowLevelCompletePanel();
            AddCoinsAnimation.instance.RewardPileOfCoin(int.Parse(crowdCounter.text));
            DataManager.instance.AddCoins(int.Parse(crowdCounter.text));
            
        }
    }
}
