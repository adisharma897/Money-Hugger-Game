using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI Tickets;
    public TextMeshProUGUI Coins;
    public TextMeshProUGUI Dollars;
    public string FAQ;
    public GameObject KenoPlay; //Reference of Keno Game

    public int CurrentScreen; //Gives the location of current screen in home
    
    public GameObject Menu; //Reference of Object of Menu

    public GameObject HomeButton; //Reference of Object of home button
    public GameObject MenuButton; //Reference of Object of Menu button

    public GameObject Screens; //Reference of Object whch holds all the screens

    public GameObject FirstScreen; //reference of opening screen
    public GameObject LoginRegisterPanel; //reference of login register screen
    public GameObject DailyRaffleObject; //reference of daily raffle object
    public GameObject AccountObject; //reference of account object

    public int InputHour;
    public int InputMin;

    public TextMeshProUGUI TimeText;

    public int Hour;
    public int Min;

    int totalmin;
    int TOTALMIN;
    bool RandomGenerator;

    DateTime CurrentTime;

    public void Start()
    {
        RandomGenerator = false;
        FirstScreen.SetActive(false);
        if(LoginMenu.IsLoggedIn)
        {
            AccountManager accountManager = AccountObject.GetComponent<AccountManager>();
            accountManager.UpdateAccountDetails();
            Tickets.text = LoginMenu.Player.tickets.ToString();
            Coins.text = LoginMenu.Player.coins.ToString();
            Dollars.text = LoginMenu.Player.dollar.ToString();
        }
        else
        {
            Tickets.text = "0";
            Coins.text = "0";
            Dollars.text = "0";
        }
        ResetTimer();
        StartCoroutine(Minute());
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetString("LastLogin", DateTime.Now.ToString());
        Debug.Log(DateTime.Now.ToString());
    }

    void Update()
    {
        LoginMenu lm = LoginRegisterPanel.GetComponent<LoginMenu>();
        if (Hour == 0 && Min == 1 && RandomGenerator == false) //random var generator when 1min is left
        {
            //StartCoroutine(lm.RandomUser());
            RandomGenerator = true;
        }
        if (Hour == 0 && Min == 0)
        {
            ResetTimer();
            //DailyRaffle script = DailyRaffleObject.GetComponent<DailyRaffle>();
            //script.RaffleTime();
        }
    }

    public void UpdateCommon(string key, int addition)
    {
        if (key.CompareTo("CommunityChestDonations") == 0)
        {
            LoginMenu.ComDataVar.CommunityChestDonations += addition;
        }
        if(key.CompareTo("NumberOfUsers")==0)
        {
            LoginMenu.ComDataVar.NumberOfUsers += addition;
        }
        LoginMenu.UploadCom();
    }
    

    public void UpdateAssets(int t, int c, int d)
    {
        int temp;
        temp = int.Parse(Tickets.text) + t;
        Tickets.text = temp.ToString();
        LoginMenu.Player.tickets = temp;
        temp = int.Parse(Coins.text) + c;
        Coins.text = temp.ToString();
        LoginMenu.Player.coins = temp;
        temp = int.Parse(Dollars.text) + d;
        Dollars.text = temp.ToString();
        LoginMenu.Player.dollar = temp;
        LoginMenu.UploadData(LoginMenu.Player);
    }

    public void SwitchScreen(int to)
    {
        if (to == 11) LoginRegisterPanel.SetActive(false);
        if (to != 4)
        {
            Screens.transform.GetChild(CurrentScreen).gameObject.SetActive(false); //disables current screen
            Screens.transform.GetChild(to).gameObject.SetActive(true); //enables new screen
        }
        else
        {
            Application.OpenURL(FAQ);
        }
        
        ToogleMenu(false);
        if (to < 11 && to != 4)
        {
            HomeButton.SetActive(true);
            MenuButton.SetActive(false);
        }
        else
        {
            HomeButton.SetActive(false);
            MenuButton.SetActive(true);
        }
        if (to == 8)
        {
            KenoPlay.SetActive(true); //Enabling Keno game
        }
        CurrentScreen = to;
    }

    public void ToogleMenu(bool TF) //enables-disables menu on press of buttons
    {
        Menu.SetActive(TF); //enables-disables menu as per the value of tf
    }

    public void ResetTimer()
    {
        CurrentTime = DateTime.Now;

        Hour = InputHour;
        Min = InputMin;

        Hour = Hour - CurrentTime.Hour;
        Min = Min - CurrentTime.Minute;
        if (Hour <= 0 && Min < 0) Hour = Hour + 23;
        if (Min < 0)
        {
            Min = Min + 60;
            Hour--;
        }

        totalmin = 0;
        TOTALMIN = 0;
        TimeText.text = Hour + ":" + Min + " Untill Next Raffle";
        if (Min > 0) totalmin += Hour * 60;
        if (Min > 0) totalmin += Min;

        TOTALMIN = totalmin;
    }

    IEnumerator Minute()
    {
        yield return new WaitForSeconds(60f);
        if (Min >= 0) Min--;

        if (Min < 0 && Hour != 0)
        {
            Min = 59;
            Hour--;
        }

        TimeText.text = Hour + ":" + Min + " Untill Next Raffle";
        StartCoroutine(Minute());
    }

    private void OnDisable()
    {
        StopCoroutine(Minute());
    }

}
