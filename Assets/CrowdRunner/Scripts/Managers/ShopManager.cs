using System;
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

    
    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            skinButtons[i].Configure(skins[i], true);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
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
}
