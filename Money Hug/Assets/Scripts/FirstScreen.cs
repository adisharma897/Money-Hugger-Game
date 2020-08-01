using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstScreen : MonoBehaviour
{
    public GameObject MainGame; //reference of main game
    public GameObject LoginRegisterPanel; //Reference of Login-register panel
    public GameObject AdManagerObject; //reference of Ad Manager object

    public void LoginPanelSwitch(bool tf) //to enable-disable Login-register panel
    {
        LoginRegisterPanel.SetActive(tf);
        SwitchLoginRegister(true);
        LoginMenu script = LoginRegisterPanel.GetComponent<LoginMenu>();
        script.ResetFields();
    }
    public void SwitchLoginRegister(bool tf) //to switch between login and register
    {
        LoginRegisterPanel.transform.GetChild(0).gameObject.SetActive(tf);
        LoginRegisterPanel.transform.GetChild(1).gameObject.SetActive(!tf);
        LoginMenu script = LoginRegisterPanel.GetComponent<LoginMenu>();
        script.ResetFields();
    }

    private void Start()
    {
        LoginMenu script = LoginRegisterPanel.GetComponent<LoginMenu>();
        script.DownloadCommon();
    }

    public void Play()
    {
        MainGame.SetActive(true);
        AdManager script = AdManagerObject.GetComponent<AdManager>();
        script.ShowBannerAd();
    }

    public void RedirectTo(string url)
    {
        Application.OpenURL(url);
    }

}
