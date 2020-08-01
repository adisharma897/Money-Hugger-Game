using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EarnMore : MonoBehaviour
{
    DateTime CurrentTime, LastLogin;
    public Button[] buttons;
    char[] arr;
    public GameObject AdManagerObject;
    public GameObject Header;
    public GameObject LoginRegisterPanel;

    private void OnEnable()
    {
        if (LoginMenu.IsLoggedIn) //if logged in
        {
            LoginRegisterPanel.SetActive(false);
        }
        else //if logged out
        {
            LoginRegisterPanel.SetActive(true);
        }
    }
    private void Start()
    {
        if (LoginMenu.IsLoggedIn) //is logged in
        {
            LoginRegisterPanel.SetActive(false);
        }
        else //is logged out
        {
            LoginRegisterPanel.SetActive(true);
        }
    }

    void OnDisable()
    {
        String str = new String(arr);
        PlayerPrefs.SetString("DataStream", str);
    }

    void InitializeButtons()
    {
        arr = new char[4];
        if (PlayerPrefs.HasKey("DataStream"))
        {
            arr = PlayerPrefs.GetString("DataStream").ToCharArray();

            CurrentTime = DateTime.Now;
            if (PlayerPrefs.HasKey("LastLogin"))
            {
                for (int i = 0; i < 4; i++)
                {
                    MakeButtonsInteractive(i, false);
                }
                if (arr[0] == '0')
                {
                    MakeButtonsInteractive(0, true);
                }
                if (arr[1] == '0')
                {
                    MakeButtonsInteractive(1, true);
                }
                if (arr[2] == '0')
                {
                    MakeButtonsInteractive(2, true);
                }
                if (arr[3] == '0')
                {
                    MakeButtonsInteractive(3, true);
                }
                if (arr[0] == '1' || arr[1] == '1' || arr[2] == '1' || arr[3] == '1')
                {
                    LastLogin = DateTime.Parse(PlayerPrefs.GetString("LastLogin"), System.Globalization.CultureInfo.CurrentCulture);
                    Debug.Log(LastLogin.ToString());

                    if (DateTime.Compare(LastLogin.Date, CurrentTime.Date) < 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            MakeButtonsInteractive(i, true);
                        }

                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            MakeButtonsInteractive(i, false);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    MakeButtonsInteractive(i, true);
                }
            }

        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                MakeButtonsInteractive(i, true);
            }
        }
    }

    void MakeButtonsInteractive(int i, bool tf)
    {
        buttons[i].interactable = tf;
    }

    public void Button1()
    {
        MenuController script = Header.GetComponent<MenuController>();
        script.UpdateAssets(0, 1, 0);
        MakeButtonsInteractive(0, false);
        arr[0] = '1';
    }

    public void Button2()
    {
        AdManager script = AdManagerObject.GetComponent<AdManager>();
        script.ShowRewardedVideoAd();
        MenuController scriptt = Header.GetComponent<MenuController>();
        scriptt.UpdateAssets(0, 2, 0);
        MakeButtonsInteractive(1, false);
        arr[1] = '1';
    }

    public void Button3()
    {
        MenuController script = Header.GetComponent<MenuController>();
        script.UpdateAssets(1, 0, 0);
        MakeButtonsInteractive(2, false);
        arr[2] = '1';
    }

    public void Button4()
    {
        AdManager script = AdManagerObject.GetComponent<AdManager>();
        script.ShowRewardedVideoAd();
        MenuController scriptt = Header.GetComponent<MenuController>();
        scriptt.UpdateAssets(2, 0, 0);
        MakeButtonsInteractive(3, false);
        arr[3] = '1';
    }
}
