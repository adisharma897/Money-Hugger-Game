using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdManager : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "3410791";
#elif UNITY_ANDROID
    private string gameId = "3410790";
#endif

    string VideoAd = "video";
    string RewardedVideoAd = "rewardedVideo";
    string BannerAd = "BannerAd";

    bool AdReady = false;
    public static bool WatchedFullAd = false;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    public void ShowVideoAd()
    {
        Advertisement.Show(VideoAd);
    }

    public void ShowRewardedVideoAd()
    {
        WatchedFullAd = false;
        if (AdReady == true)
        {
            Advertisement.Show(RewardedVideoAd);
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:

        if (showResult == ShowResult.Finished) // Reward the user for watching the ad to completion.
        {
            WatchedFullAd = true;
            Debug.Log("Rewarded");
        }
        else if (showResult == ShowResult.Skipped) // Do not reward the user for skipping the ad.
        {
            WatchedFullAd = false;
        }
        else if (showResult == ShowResult.Failed)
        {
            
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        
        // If the ready Placement is rewarded, show the ad:
        if (placementId == RewardedVideoAd)
        {
            AdReady = true;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void ShowBannerAd()
    {
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(BannerAd))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        for (int i = 0; i < 10; i++)
        {
            Advertisement.Banner.Show(BannerAd);
            yield return new WaitForSeconds(30.0f);
        }
    }

}
