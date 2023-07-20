using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button skinButton;
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selector;

    private bool isUnlocked = false;

    public void Configure(Sprite skinSprite, bool unlocked)
    {
        this.isUnlocked = unlocked;
        skinImage.sprite = skinSprite;

        if (unlocked)
            Unlocked();
        else
            Lock();
    }

    private void Lock()
    {
        skinButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);
    }

    public void Unlocked()
    {
        skinButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);

        isUnlocked = true;
    }

    public void Selcet()
    {
        selector.SetActive(true);
    }

    public void Deselect()
    {
        selector.SetActive(false);
    }

    public Button GetButton()
    {
        return skinButton;
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }
}
