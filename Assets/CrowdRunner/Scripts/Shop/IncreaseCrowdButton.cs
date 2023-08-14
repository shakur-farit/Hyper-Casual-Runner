using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncreaseCrowdButton : ShopButton
{
    [SerializeField] private int increaseAmount;
    [SerializeField] private TMP_Text increaseAmountText;

    [SerializeField] CrowdSystem crowdSystem;

    private void Start()
    {
        increaseAmountText.text = "+ " + increaseAmount.ToString();
        crowdSystem = FindObjectOfType<CrowdSystem>();
    }

    public void IncreaseCrowd()
    {
        crowdSystem.CrowdOnStart(increaseAmount);
        DataManager.instance.RemoveCoins(price);
    }
}
