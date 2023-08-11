using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private TMP_Text purchaseButtonPriceText;

    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject useButton;

    private int selectedSkinIndex = 0;

    void Start()
    {
        UnlockOnStart();
        ConfigureButtons();
    }


    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;

            skinButtons[i].Configure(unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton.onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    private void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlocked();
    }

    private void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
            {
                skinButtons[i].Selcet();
                purchaseButtonPriceText.text = skinButtons[i].SkinPrice.ToString();
                ButtonUpdate(i);
                selectedSkinIndex = i;
            }
            else
                skinButtons[i].Deselect();
        }
    }

    public void PurchaseSkin()
    {
        if (DataManager.instance.Coins < skinButtons[selectedSkinIndex].SkinPrice)
            return;

        if (!skinButtons[selectedSkinIndex].IsUnlocked)
        {
            UnlockSkin(selectedSkinIndex);
            DataManager.instance.RemoveCoins(skinButtons[selectedSkinIndex].SkinPrice);
            Debug.Log("Unlocked " + selectedSkinIndex + " button");
            ButtonUpdate(selectedSkinIndex);
        }
        else
            return;
    }

    private void ButtonUpdate(int index)
    {
        if (skinButtons[index].IsUnlocked)
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
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinButtons[i].IsUnlocked)
                UnlockSkin(i);
        }
    }
}
