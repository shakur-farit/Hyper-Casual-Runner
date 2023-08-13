using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCrowdButton : ShopButton
{
    [SerializeField] private int increaseAmount;

    [SerializeField] CrowdSystem crowdSystem;

    private void Start()
    {
        crowdSystem = FindObjectOfType<CrowdSystem>();
    }

    public void IncreaseCrowd()
    {
        crowdSystem.CrowdAmountOnStart = increaseAmount;
        DataManager.instance.RemoveCoins(price);
    }
}
