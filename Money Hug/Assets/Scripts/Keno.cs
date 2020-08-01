﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keno : MonoBehaviour
{
    int[] SelectedNumbers; //list of number selected by user
    int n; //count of selected numbers
    int WinningNumber = 0; //number of winning numbers

    public GameObject AdManagerObject;

    public GameObject KenoBoard; //Reference of object which holds all tiles
    public TextMeshProUGUI WinningText;
    public GameObject WinningTextBackground;

    public GameObject Header;

    public GameObject PlayButton;
    public GameObject ContinueButton;
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
        n = 0;
        SelectedNumbers = new int[5]; //5 numbers to be selected by user
        WinningNumber = 0;
        for (int i = 0; i < 80; i++) ChangeColor(KenoBoard.transform.GetChild(i).gameObject, 0);
        WinningText.text= "No. of Winning Numbers: \nNo. of coins won: " ;
        WinningTextBackground.SetActive(false);

        PlayButton.SetActive(true);
        ContinueButton.SetActive(false);
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) //for mobile
        {
            if (Input.touchCount > 0 && Input.touchCount < 2) //on touch
            {
                if(Input.GetTouch(0).phase==TouchPhase.Began)
                {
                    CheckTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) //for pc
        {
            if (Input.GetMouseButtonDown(0)) //on mouse button down
            {
                CheckTouch(Input.mousePosition);
            }
        }
    }

    void CheckTouch(Vector2 Position)
    {
        Vector2 WorldPosition = Camera.main.ScreenToWorldPoint(Position);
        RaycastHit2D hit = Physics2D.Raycast(WorldPosition, Vector2.zero); //receiving hit on raycast

        if (hit.collider != null)
        {
            CheckTile(hit.collider.gameObject); //checking if it was a valid hit
        }
    }

    private void CheckTile(GameObject hit)
    {
        TextMeshPro tmp;
        if (hit.transform.childCount > 0 && n < 5) //if hit has a child and user has made less than 5 trials
        {
            ChangeColor(hit, 1); //changing color to red on selection by user
            tmp = hit.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
            SelectedNumbers[n] = int.Parse(tmp.text); //retrieving number attached to hit object
            n++; //incrementing trials
        }
    }

    private void ChangeColor(GameObject g, int c)
    {
        SpriteRenderer s = g.GetComponent<SpriteRenderer>();
        if (c == 0) //blue for default
        {
            s.color = new Color(0.0f, 0.0f, 1.0f);
        }
        if (c == 1) //red for selection
        {
            s.color = new Color(1.0f, 0.0f, 0.0f);
        }
        if (c == 2) //yellow for win
        {
            s.color = new Color(1.0f, 1.0f, 0.0f);
        }
        if (c == 3) //green for number generated by computer
        {
            s.color = new Color(0.0f, 1.0f, 0.0f);
        }
    }

    public void PlayKeno()
    {
        int i;
        List<int> numbers = new List<int>(81); //list
        for (i = 0; i < 80; i++)
        {
            numbers.Add(i + 1); //adding numbers from 1-80 to list
        }
        int[] randomNumbers = new int[10]; //contains random numbers generated by computer
        for (i = 0; i < randomNumbers.Length; i++) //algorithm to generate random numbers
        {
            int thisNumber = Random.Range(1, numbers.Count);
            randomNumbers[i] = numbers[thisNumber];
            numbers.RemoveAt(thisNumber);
        }

        for (i = 0; i < 10; i++)
        {
            ChangeColor(KenoBoard.transform.GetChild(randomNumbers[i]-1).gameObject, 3); //random number color to green
            for (int j = 0; j < 5; j++) //to check each random number with each selected number
            {
                if(SelectedNumbers[j] == randomNumbers[i]) //if there exists a same number
                {
                    ChangeColor(KenoBoard.transform.GetChild(randomNumbers[i] - 1).gameObject, 2); //changing color to yellow
                    WinningNumber++; //incrementing number of winning number
                    break;
                }
            }
        }
        WinningTextBackground.SetActive(true);
        WinningText.text = "No. of Winning Numbers: " + WinningNumber + "\nNo. of coins won: " + (WinningNumber * 3);
        MenuController script = Header.GetComponent<MenuController>();
        script.UpdateAssets(0, WinningNumber * 3, 0);

        PlayButton.SetActive(false);
        ContinueButton.SetActive(true);
    }

    public void Continue()
    {
        AdManager Ad = AdManagerObject.GetComponent<AdManager>();
        Ad.ShowRewardedVideoAd();
        OnEnable();
    }
}