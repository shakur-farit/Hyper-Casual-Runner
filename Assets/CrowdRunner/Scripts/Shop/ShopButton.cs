using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] private GameObject lockImage;
    [SerializeField] protected GameObject selector;
    [SerializeField] protected int price = 10;

    public int Price => price;

    [SerializeField] private bool isUnlocked = false;

    public bool IsUnlocked => isUnlocked;
    public Button GetButton => button;

    public void Configure(bool unlocked)
    {
        isUnlocked = unlocked;

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
}
