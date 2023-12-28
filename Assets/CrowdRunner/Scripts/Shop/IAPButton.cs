using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IAPButton : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsAmountText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int coinsAmount;
    [SerializeField] private float price;

    private void Start()
    {
        coinsAmountText.text = coinsAmount.ToString();
        priceText.text = price.ToString() + "$";
    }
}
