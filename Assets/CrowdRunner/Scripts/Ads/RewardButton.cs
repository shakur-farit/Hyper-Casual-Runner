using UnityEngine;

public class RewardButton : MonoBehaviour
{
    public void ShowRewardAd()
    {
        AdInitialize.instance.rewardAd.ShowRewardedAd();
    }
}
