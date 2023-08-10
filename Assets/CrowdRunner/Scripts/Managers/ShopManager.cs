using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private TMP_Text purchaseButtonPriceText;

    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private GameObject useButton;

    public int selectedSkin = 0;

    void Start()
    {
        UnlockOnStart();
        ConfigureButtons();
    }

    private void Update()
    {
        ButtonUpdate();
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
                selectedSkin = i;
            }
            else
                skinButtons[i].Deselect();
        }
    }

    public void PurchaseSkin()
    {
        if (DataManager.instance.Coins < skinButtons[selectedSkin].SkinPrice)
            return;

        if (!skinButtons[selectedSkin].IsUnlocked)
        {
            UnlockSkin(selectedSkin);
            DataManager.instance.RemoveCoins(skinButtons[selectedSkin].SkinPrice);
            Debug.Log("Unlocked " + selectedSkin + " button");
        }
        else
            return;
    }

    private void ButtonUpdate()
    {
        if (skinButtons[selectedSkin].IsUnlocked)
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
