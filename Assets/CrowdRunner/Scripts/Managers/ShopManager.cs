using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopButton[] shopButtons;
    [SerializeField] private TMP_Text purchaseButtonPriceText;

    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject useButton;

    [SerializeField] private PlayerSelector playerSelector;

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
                purchaseButtonPriceText.text = shopButtons[i].Price.ToString();
                ButtonUpdate(i);
                selectedSkinIndex = i;
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
        if (shopButtons[index].IsUnlocked)
        {
            purchaseButton.SetActive(false);
            useButton.SetActive(true);
        }
        else
        {
            purchaseButton.SetActive(true);
            useButton.SetActive(false);
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
        if (shopButtons[selectedSkinIndex].GetComponent<IncreaseCrowdButton>() != null)
            shopButtons[selectedSkinIndex].GetComponent<IncreaseCrowdButton>().IncreaseCrowd();
        else
            playerSelector.SelectSkin(selectedSkinIndex);
    }
}
