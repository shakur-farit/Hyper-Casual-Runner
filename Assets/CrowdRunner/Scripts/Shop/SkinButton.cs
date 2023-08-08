using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button skinButton;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selector;
    [SerializeField] private int skinPrice = 10;

    public int SkinPrice { get { return skinPrice; } }

    private bool isUnlocked = false;

    public void Configure(bool unlocked)
    {
        this.isUnlocked = unlocked;

        if (unlocked)
            Unlocked();
        else
            Lock();
    }

    private void Lock()
    {
        lockImage.SetActive(true);
    }

    public void Unlocked()
    {
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
