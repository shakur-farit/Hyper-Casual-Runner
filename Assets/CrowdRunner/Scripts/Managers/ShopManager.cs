using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private TMP_Text purchaseButtonPriceText;

    public int selectedSkin = 0;

    void Start()
    {
        ConfigureButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UnlockSkin(Random.Range(0, skinButtons.Length));
        if (Input.GetKeyDown(KeyCode.D))
            PlayerPrefs.DeleteAll();
    }


    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;

            skinButtons[i].Configure(unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
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
                purchaseButtonPriceText.text = skinButtons[i].GetSkinPrice().ToString();
                selectedSkin = i;
            }
            else
                skinButtons[i].Deselect();
        }
    }

    public void PurchaseSkin()
    {
        if (!skinButtons[selectedSkin].IsUnlocked())
        {
            UnlockSkin(selectedSkin);
            Debug.Log("Unlocked " + selectedSkin + " button");
        }
        else
            return;
    }
}
