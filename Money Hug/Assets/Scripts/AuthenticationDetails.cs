using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class AuthenticationDetails
{
    public int UserID;
    public string Username;
    public string Password;

    public AuthenticationDetails()
    {
        Username = "";
        Password = "";
        UserID = 0;
    }
    public AuthenticationDetails(string InputUsername, string InputPassword, int ID)
    {
        Username = InputUsername;
        Password = InputPassword;
        UserID = ID;
    }
    public void CopyDetailFromUser(User user)
    {
        UserID = user.UserID;
        Username = user.Username;
        Password = user.Password;
    }
}
