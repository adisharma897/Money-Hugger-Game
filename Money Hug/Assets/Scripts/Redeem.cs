using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Mail;

public class Redeem : MonoBehaviour
{
    public string Email; //company email
    public TMP_InputField InputF; //reference of input field
    public GameObject Header; //reference of header
    public GameObject ResultTextBackground; //reference of background of text to display result
    public TextMeshProUGUI ResultText; //reference of text to display result
    public GameObject LoginScreen; //reference of login screen

    private void OnEnable()
    {
        ResultTextBackground.SetActive(false);

        if (LoginMenu.IsLoggedIn) //is logged in
        {
            LoginScreen.SetActive(false);
        }
        else //is logged out
        {
            LoginScreen.SetActive(true);
        }
    }

    public void RedeemAmount(string Mode) //function checks if amount entered is less than amount user has, sends email to company on button press, different buttons have different modes
    {
        int amount = int.Parse(InputF.text);
        if (LoginMenu.Player.dollar >= amount) //if enough money
        {
            MenuController script = Header.GetComponent<MenuController>();
            script.UpdateAssets(0, 0, amount * -1); //updating of assets in server
            email(amount.ToString(), Mode);
            ResultTextBackground.SetActive(true);
            ResultText.text = "Success!! Amount will be transferred in 24 hours.";
        }
        else
        {
            ResultTextBackground.SetActive(true);
            ResultText.text = "Not enough Money";
        }
    }

    void email(string amount, string mode)
    {
        MailMessage Mail = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        Mail.To.Add(new MailAddress(Email));
        Mail.From = new MailAddress("Adisharma897@gmail.com");
        Mail.Subject = "Redeem Request";
        Mail.IsBodyHtml = true;
        Mail.Body = amount + " through " + mode + " to " + LoginMenu.Player.Email;
        smtp.Port = 587;
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(Mail);
        
    }

}
