using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccountManager : MonoBehaviour
{
    public TextMeshProUGUI Ticket, coin, dollar, email, username;
    public GameObject LoginScreen;
    public GameObject Header;

    private void OnEnable()
    {
        if(LoginMenu.IsLoggedIn) //if logged in
        {
            LoginScreen.SetActive(false);
        }
        else //if logged out
        {
            LoginScreen.SetActive(true);
        }
    }
    
    public void LogOut()
    {
        LoginMenu.IsLoggedIn = false;
        LoginScreen.SetActive(true);
        LoginMenu.Player = new User();
        MenuController script = Header.GetComponent<MenuController>();
        script.Start();
    }

    public void UpdateAccountDetails()
    {
        username.text = LoginMenu.Player.Username;
        email.text = LoginMenu.Player.Email;
        Ticket.text = LoginMenu.Player.tickets.ToString();
        coin.text = LoginMenu.Player.coins.ToString();
        dollar.text = LoginMenu.Player.dollar.ToString();
    }
}
