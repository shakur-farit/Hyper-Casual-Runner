using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopButton[] shopButtons;
    [SerializeField] private TMP_Text purchaseButtonPriceText;
    [SerializeField] private TMP_Text useButtonPriceText;

    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject useWithPriceButton;

    [SerializeField] private PlayerSelector playerSelector;
    [SerializeField] private CrowdSystem crowdSystem;

    private int selectedSkinIndex = 0;

    void Start()
    {
        UnlockOnStart();
        ConfigureButtons();
    }


    private void ConfigureButtons()
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("shopButton" + i) == 1;

            shopButtons[i].Configure(unlocked);

            int skinIndex = i;

            shopButtons[i].GetButton.onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    private void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("shopButton" + skinIndex, 1);
        shopButtons[skinIndex].Unlocked();
    }

    private void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            if (skinIndex == i)
            {
                shopButtons[i].Selcet();
                
                if(shopButtons[i].GetComponent<IncreaseCrowdButton>())
                    useButtonPriceText.text = shopButtons[i].Price.ToString();
                else
                    purchaseButtonPriceText.text = shopButtons[i].Price.ToString();
                
                selectedSkinIndex = i;
                ButtonUpdate(i);
            }
            else
                shopButtons[i].Deselect();
        }
    }

    public void PurchaseSkin()
    {
        if (DataManager.instance.Coins < shopButtons[selectedSkinIndex].Price)
            return;

        if (!shopButtons[selectedSkinIndex].IsUnlocked)
        {
            UnlockSkin(selectedSkinIndex);
            DataManager.instance.RemoveCoins(shopButtons[selectedSkinIndex].Price);
            Debug.Log("Unlocked " + selectedSkinIndex + " button");
            ButtonUpdate(selectedSkinIndex);
        }
        else
            return;
    }

    private void ButtonUpdate(int index)
    {
        if(shopButtons[index].IsUnlocked && shopButtons[index].GetComponent<IncreaseCrowdButton>())
        {
            purchaseButton.SetActive(false);
            useButton.SetActive(false);
            useWithPriceButton.SetActive(true);

            if (DataManager.instance.Coins < shopButtons[selectedSkinIndex].Price)
            {
                useWithPriceButton.GetComponent<Button>().interactable = false;
                useWithPriceButton.GetComponent<ButtonTextItems>().label.SetActive(false);
                useWithPriceButton.GetComponent<ButtonTextItems>().notEnoughCoinsText.SetActive(true);
            }
            else
            {
                useWithPriceButton.GetComponent<Button>().interactable = true;
                useWithPriceButton.GetComponent<ButtonTextItems>().label.SetActive(true);
                useWithPriceButton.GetComponent<ButtonTextItems>().notEnoughCoinsText.SetActive(false);
            }
        }
        else if(shopButtons[index].IsUnlocked)
        {
            purchaseButton.SetActive(false);
            useButton.SetActive(true);
            useWithPriceButton.SetActive(false);
        }
        else
        {
            purchaseButton.SetActive(true);
            useButton.SetActive(false);
            useWithPriceButton.SetActive(false);

            if (DataManager.instance.Coins < shopButtons[selectedSkinIndex].Price)
            {
                purchaseButton.GetComponent<Button>().interactable = false;
                purchaseButton.GetComponent<ButtonTextItems>().label.SetActive(false);
                purchaseButton.GetComponent<ButtonTextItems>().notEnoughCoinsText.SetActive(true);
            }
            else
            {
                purchaseButton.GetComponent<Button>().interactable = true;
                purchaseButton.GetComponent<ButtonTextItems>().label.SetActive(true);
                purchaseButton.GetComponent<ButtonTextItems>().notEnoughCoinsText.SetActive(false);
            }
        }
    }

    private void UnlockOnStart()
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            if (shopButtons[i].IsUnlocked)
                UnlockSkin(i);
        }
    }

    public void UseButton()
    {
        playerSelector.SelectSkin(selectedSkinIndex);
    }

    public void UseWithPriceButton()
    {
        shopButtons[selectedSkinIndex].GetComponent<IncreaseCrowdButton>().IncreaseCrowd();
    }
}
