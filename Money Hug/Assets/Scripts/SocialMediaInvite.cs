using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaInvite : MonoBehaviour
{
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
    }
    public void Invite(string link) //Function opens invite link which is to be provided by the company, it also makes changes to coins/assets on invitation
    {
        Application.OpenURL(link);
        MenuController m = new MenuController();
        m.UpdateAssets(0, 1000, 0);
    }
}
