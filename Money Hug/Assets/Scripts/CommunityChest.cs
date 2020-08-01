using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommunityChest : MonoBehaviour
{
    public GameObject DonatePanel;
    public GameObject WinnerPanel;

    public GameObject LoginRegisterPanel; //Reference of Login-register panel

    public GameObject Header;
    public GameObject AdManagerObject;
    public GameObject Error;

    public TextMeshProUGUI Winner; //winner name

    public TMP_InputField DonationInput;
    int donation;

    private void OnEnable()
    {
        Winner.text = LoginMenu.ComDataVar.CurrentWinnerCommunityChest;
        if (LoginMenu.IsLoggedIn) //if logged in
        {
            LoginRegisterPanel.SetActive(false);
        }
        else //if logged out
        {
            LoginRegisterPanel.SetActive(true);
        }
        if (LoginMenu.ComDataVar.CommunityChestDonations >= 1100)
        {
            DrawWinner();
        }
        DonatePanelSwitch(false);
        WinnerPanelSwitch(false);
        Error.SetActive(false);
        AdManager Ad = AdManagerObject.GetComponent<AdManager>();
        Ad.ShowRewardedVideoAd();
    }

    public void Donate()
    {
        donation = int.Parse(DonationInput.text);
        if (donation <= LoginMenu.Player.dollar) //if sufficient amount
        {
            MenuController script = Header.GetComponent<MenuController>();
            script.UpdateAssets(0, 0, donation * -1);
            script.UpdateCommon("CommunityChestDonations", donation);
            DonatePanelSwitch(false);
            Error.SetActive(false);
        }
        else //if insufficient amount
        {
            Error.SetActive(true);
        }
    }

    public void DonatePanelSwitch(bool tf)
    {
        DonatePanel.SetActive(tf);
        Error.SetActive(false);
    }

    public void WinnerPanelSwitch(bool tf)
    {
        WinnerPanel.SetActive(tf);
    }

    void DrawWinner()
    {
        Winner.text = LoginMenu.RandomPlayerVar.Username;
        LoginMenu.ComDataVar.CurrentWinnerCommunityChest = LoginMenu.RandomPlayerVar.Username;
        LoginMenu.RandomPlayerVar.dollar += 1000;
        LoginMenu.ComDataVar.CommunityChestDonations -= 1100;
        LoginMenu.UploadData(LoginMenu.RandomPlayerVar);
        LoginMenu.UploadCom();
    }
}
