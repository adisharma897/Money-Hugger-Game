using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratchers : MonoBehaviour
{
    public GameObject Prizes;
    public GameObject Cover;
    public GameObject Header;

    public GameObject ContinueButton;
    public GameObject CollectPrizeButton;
    public GameObject AdManagerObject;
    public GameObject LoginScreen;

    private void OnEnable()
    {
        if (LoginMenu.IsLoggedIn) //is logged in
        {
            LoginScreen.SetActive(false);
        }
        else //is logged out
        {
            LoginScreen.SetActive(true);
        }
        for (int i = 0; i < 4; i++) Prizes.transform.GetChild(i).gameObject.SetActive(false);
        Cover.SetActive(true);
        CollectPrizeButton.SetActive(true);
        ContinueButton.SetActive(false);
    }
    
    public void CollectPrize()
    {
        int RandomPrize = Random.Range(0, 4);
        Prizes.transform.GetChild(RandomPrize).gameObject.SetActive(true);
        Cover.SetActive(false);
        prize(RandomPrize);
        CollectPrizeButton.SetActive(false);
        ContinueButton.SetActive(true);
    }

    void prize(int n)
    {
        int ticket = 0, dollar = 0, coin = 0;
        switch(n)
        {
            case 0:
                { ticket = 1;break; }
            case 1:
                { dollar = 1; break; }
            case 2:
                { coin = 1; break; }
        }
        MenuController script = Header.GetComponent<MenuController>();
        script.UpdateAssets(ticket, coin, dollar);
    }

    public void Continue()
    {
        AdManager Ad = AdManagerObject.GetComponent<AdManager>();
        Ad.ShowRewardedVideoAd();
        OnEnable();
    }
}
