using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class User
{
    public int UserID;
    public string Username;
    public string Password;
    public string Email;
    public int tickets;
    public int coins;
    public int dollar;

    public User()
    {
        Username = "";
        Password = "";
        Email = "";
        tickets = 0;
        coins = 0;
        dollar = 0;
        UserID = 0;
    }
    public User(string InputUsername, string InputPassword, string InputEmail, int ID)
    {
        Username = InputUsername;
        Password = InputPassword;
        Email = InputEmail;
        tickets = 0;
        coins = 0;
        dollar = 0;
        UserID = ID;
    }

    public User(string InputUsername, string InputPassword, string InputEmail)
    {
        Username = InputUsername;
        Password = InputPassword;
        Email = InputEmail;
    }

    public User(string InputEmail, string InputPassword)
    {
        Password = InputPassword;
        Email = InputEmail;
    }

    public User(string InputUsername, string InputPassword, string InputEmail, int InputTickets, int InputCoins, int InputDollar, int ID)
    {
        Username = InputUsername;
        Password = InputPassword;
        Email = InputEmail;
        tickets = InputTickets;
        coins = InputCoins;
        dollar = InputDollar;
        UserID = ID;
    }
}
