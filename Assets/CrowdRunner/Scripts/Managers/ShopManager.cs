using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Sprite[] skins;
    
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

            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlocked();
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    private void SelectSkin(int skinIndex)
    {
        //Debug.Log("Skin " + skinIndex + "has been selceted");

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
                skinButtons[i].Selcet();
            else
                skinButtons[i].Deselect();
        }
    }

    public void PurchaseSkin()
    {
        List<SkinButton> skinButtonList = new List<SkinButton>();

        for (int i = 0; i < skinButtons.Length; i++)
            if (!skinButtons[i].IsUnlocked())
                skinButtonList.Add(skinButtons[i]);

//        Debug.Log(skinButtonList[2]);

        if (skinButtonList.Count <= 0)
            return;

        SkinButton randomSkinButton = skinButtonList[Random.Range(0, skinButtonList.Count)];

        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());
    }
}
