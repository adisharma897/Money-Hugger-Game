using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DailyRaffle : MonoBehaviour
{
    public GameObject WinnerPanel;
    public TextMeshProUGUI WinnerText;
    public GameObject Header;

    public GameObject LoginRegisterPanel;
    public GameObject AdManagerObject;
    public int DailyRafflePrize;

    private void OnEnable()
    {
        AdManager script = AdManagerObject.GetComponent<AdManager>();
        script.ShowRewardedVideoAd();
        WinnerPanel.SetActive(false);
        WinnerText.text = LoginMenu.ComDataVar.CurrentWinnerDailyRaffle;

        if(LoginMenu.IsLoggedIn) //if logged in
        {
            LoginRegisterPanel.SetActive(false);
        }
        else //if logged out
        {
            LoginRegisterPanel.SetActive(true);
        }
    }
    
    

    public void CheckWinner(bool tf)
    {
        WinnerPanel.SetActive(tf);
    }
      
    /*
    public void RaffleTime()
    {
        WinnerText.text = LoginMenu.RandomPlayerVar.Username;
        LoginMenu.ComDataVar.CurrentWinnerDailyRaffle = LoginMenu.RandomPlayerVar.Username;
        LoginMenu.RandomPlayerVar.dollar += DailyRafflePrize;
        LoginMenu.UploadData(LoginMenu.RandomPlayerVar);
        LoginMenu.UploadCom();
    }
    */
}
