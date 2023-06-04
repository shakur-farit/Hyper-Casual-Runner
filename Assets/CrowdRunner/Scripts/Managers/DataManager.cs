using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [SerializeField] private TMP_Text[] coinsText;
    private int coins;

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

        UpdateCoinsText();

        PlayerPrefs.SetInt("Coins", coins);
    }

    private void UpdateCoinsText()
    {
        foreach(TMP_Text coinText in coinsText)
        {
            coinText.text = coins.ToString();
        }
    }
}
