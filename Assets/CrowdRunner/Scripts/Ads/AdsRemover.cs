using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdsRemover : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private float price;
    [SerializeField] private TMP_Text priceText;

    [SerializeField] private GameObject activeBox;
    [SerializeField] private TMP_Text soldText;


    private void Start()
    {
        priceText.text = price.ToString() + " $";
        UpdateAds();
    }

    public void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        Debug.Log("Remove ads");
        UpdateAds();
        AdInitialize.instance.AdsRemovedCheck();
    }

    private void UpdateAds()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            UpdateRemoveAdsShopButton();
            UpdateText();
        }
    }

    private void UpdateRemoveAdsShopButton()
    {
        button.interactable = false;
    }

    private void UpdateText()
    {
        activeBox.SetActive(false);
        soldText.gameObject.SetActive(true);
    }
}
