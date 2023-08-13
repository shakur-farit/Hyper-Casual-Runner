using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [SerializeField] private TMP_Text[] coinsText;

    private int coins;

    public int Coins => coins;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        coins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void Start()
    {
        UpdateCoinsText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        PlayerPrefs.SetInt("Coins", coins);

        SmoothlyUpdate();
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        if (coins < 0)
            coins = 0;

        PlayerPrefs.SetInt("Coins", coins);

        SmoothlyUpdate();
    }

    private void UpdateCoinsText()
    {
        foreach (TMP_Text coinText in coinsText)
        {
            coinText.text = coins.ToString();
        }
    }

    private void SmoothlyUpdate()
    {
        foreach (TMP_Text coinText in coinsText)
        {
            int currentCoinsValue = int.Parse(coinText.text);
            DOTween.To(() => currentCoinsValue, x => currentCoinsValue = x, coins, 1f)
                .OnUpdate(() => coinText.text = currentCoinsValue.ToString())
                .SetEase(Ease.OutSine);
        }
    }
}
