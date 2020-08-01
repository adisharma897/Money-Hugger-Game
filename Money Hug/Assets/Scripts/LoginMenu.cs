using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;

public class LoginMenu : MonoBehaviour
{
    public static bool IsLoggedIn = false; //tells if user is logged in or not
    public TextMeshProUGUI ErrorSpace; //space to display error
    int State = 0; //error code
    public GameObject LoadingText;

    public static CommonData ComDataVar;
    public static User Player;
    public static User RandomPlayerVar;
    public static AuthenticationDetails Auth;
    public GameObject FirScreen;
    public GameObject Header;
   
    public TMP_InputField LoginEmailInput; //Input of email
    public TMP_InputField LoginPasswordInput; //input of password

    public TMP_InputField RegisterUsernameInput; //Input of username
    public TMP_InputField RegisterPasswordInput; //input of password
    public TMP_InputField RegisterEmailInput; //input of email
    public Button BackButton; //Reference of back button of LoginRegisterPanel

    static string DefaultUrl = "https://game-65af7.firebaseio.com/";
    static string AuthUrl = "https://game-65af7.firebaseio.com/Authentication";
    static string PlayerDataUrl = "https://game-65af7.firebaseio.com/PlayerData";

    private void OnEnable()
    {
        if (FirScreen.activeSelf == false)
        {
            BackButton.interactable = false;
        }
        else
        {
            BackButton.interactable = true;
        }
        ResetFields();
        ErrorSpace.gameObject.SetActive(false);
        LoadingText.SetActive(false);
    }

    private void Start()
    {
        Player = new User();
        ComDataVar = new CommonData();

        ErrorSpace.gameObject.SetActive(false);

        //DownloadCommon();
    }
    /*
    public IEnumerator RandomUser()
    {
        int RandomPlayer = Random.Range(1, ComDataVar.NumberOfUsers + 1);
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(PlayerDataUrl);
        reference.Child(RandomPlayer.ToString()).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                RandomPlayerVar = new User(
                    snapshot.Child("Username").Value.ToString(),
                    snapshot.Child("Password").Value.ToString(),
                    snapshot.Child("Email").Value.ToString(),
                    int.Parse(snapshot.Child("tickets").Value.ToString()),
                    int.Parse(snapshot.Child("coins").Value.ToString()),
                    int.Parse(snapshot.Child("dollar").Value.ToString()),
                    int.Parse(snapshot.Child("UserID").Value.ToString()));
            }
        });
        yield return new WaitForSeconds(1.0f);
    }
    */

    public void ResetFields()
    {
        LoginEmailInput.text = "";
        LoginPasswordInput.text = "";
        RegisterUsernameInput.text = "";
        RegisterPasswordInput.text = "";
        RegisterEmailInput.text = "";
    }


    #region Register

    public void RegisterButton()
    {
        Player = new User();
        string username = RegisterUsernameInput.text;
        string password = RegisterPasswordInput.text;
        string email = RegisterEmailInput.text;
        if (username == "" || password == "" || email == "")
        {
            ErrorMessageGenerator("Cannot leave field empty");
        }
        else
        {
            Player = new User(username, password, email, ComDataVar.NumberOfUsers + 1);

            var registerRequest = new RegisterPlayFabUserRequest { Email = Player.Email, Password = Player.Password, Username = Player.Username };
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);

            FirstScreen script = FirScreen.GetComponent<FirstScreen>();
            script.SwitchLoginRegister(true);
        }
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        ErrorMessageGenerator(error.ErrorMessage);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered");
        UploadData(Player);

        MenuController script = Header.GetComponent<MenuController>();
        script.UpdateCommon("NumberOfUsers", 1);
    }


    #endregion Register


    #region Login

    public void LoginButton()
    {
        Player = new User();
        string email = LoginEmailInput.text;
        string password = LoginPasswordInput.text;
        if (email == "" || password == "")
        {
            State = 3;
            ErrorMessageGenerator("Cannot leave field empty");
        }
        else
        {
            Player = new User(email, password);

            var request = new LoginWithEmailAddressRequest { Email = Player.Email, Password = Player.Password };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
    }

    private void OnLoginFailure(PlayFabError error)
    {
        ErrorMessageGenerator(error.ErrorMessage);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged In");
        var req = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(req, OnGetAccountInfoSuccess, OnPlayFabCallbackError);
        DownloadData();
    }

    void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        Player.Username = result.AccountInfo.Username;
    }
    void OnPlayFabCallbackError(PlayFabError error)
    {

    }

    #endregion Login


    #region PlayerData

    public static void UploadData(User user)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "UserID", Value = Player.UserID },
                new StatisticUpdate { StatisticName = "Ticket", Value = Player.tickets },
                new StatisticUpdate { StatisticName = "Coin", Value = Player.coins },
                new StatisticUpdate { StatisticName = "Dollar", Value = Player.dollar },

            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => {
            Debug.LogError(error.GenerateErrorReport());
        });
        Player = new User();
    }

    void DownloadData()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnDataDownload,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnDataDownload(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "UserID":
                    Player.UserID = eachStat.Value;
                    break;
                case "Ticket":
                    Player.tickets = eachStat.Value;
                    break;
                case "Coin":
                    Player.coins = eachStat.Value;
                    break;
                case "Dollar":
                    Player.dollar = eachStat.Value;
                    break;
            }
        }
        LoggedIn();
        FirstScreen script = FirScreen.GetComponent<FirstScreen>();
        script.Play();
    }

    #endregion PlayerData


    #region CommonData

    #region Upload
    public static void UploadCom()
    {
        string Email = "commondata@common.data";
        string Password = "CommonData";
        var request = new LoginWithEmailAddressRequest { Email = Email, Password = Password };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnComLoginSuccessU, OnComLoginFailureU);
    }

    private static void OnComLoginFailureU(PlayFabError error)
    {

    }

    private static void OnComLoginSuccessU(LoginResult result)
    {
        Debug.Log("Common Logged IN");
        //Sends a request to save the new player data to the playfab cloud
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                //key value pair, saving the allskins array as a string to the playfab cloud
                {"CommunityChestDonations", ComDataVar.CommunityChestDonations.ToString()},
                {"NumberOfUsers", ComDataVar.NumberOfUsers.ToString()},
                {"CurrentWinnerCommunityChest", ComDataVar.CurrentWinnerCommunityChest},
                {"CurrentWinnerDailyRaffle", ComDataVar.CurrentWinnerDailyRaffle}
            }
        }, SetDataSuccess, SetDataFailure);
    }

    static void SetDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Common Data updated");
    }
    static void SetDataFailure(PlayFabError error)
    {

    }
    #endregion Upload

    #region Download
    public void DownloadCommon()
    {
        ComDataVar = new CommonData();
        string Email = "commondata@common.data";
        string Password = "CommonData";
        var request = new LoginWithEmailAddressRequest { Email = Email, Password = Password };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnComLoginSuccessD, OnComLoginFailureD);
    }

    private void OnComLoginFailureD(PlayFabError error)
    {
        ErrorMessageGenerator(error.ErrorMessage);
    }

    private void OnComLoginSuccessD(LoginResult result)
    {
        Debug.Log("Common Logged IN");
        //sends a request to get the player data from the playfab cloud
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = result.PlayFabId,
            Keys = null
        }, ComDataSuccess, ComDataFailure);
    }

    //the return callback function for success.
    void ComDataSuccess(GetUserDataResult result)
    {
        if (result.Data == null)
        {
            ErrorMessageGenerator("No Game Data Found Online");
        }
        else
        {
            ComDataVar.CommunityChestDonations = int.Parse(result.Data["CommunityChestDonations"].Value);
            ComDataVar.NumberOfUsers = int.Parse(result.Data["NumberOfUsers"].Value);
            ComDataVar.CurrentWinnerCommunityChest = result.Data["CurrentWinnerCommunityChest"].Value;
            ComDataVar.CurrentWinnerDailyRaffle = result.Data["CurrentWinnerDailyRaffle"].Value;
        }
    }

    void ComDataFailure(PlayFabError error)
    {
        ErrorMessageGenerator(error.ErrorMessage);
    }
    #endregion Download

    #endregion CommonData

    void ErrorMessageGenerator(string ErrorMessage)
    {
        ErrorSpace.gameObject.SetActive(true);
        ErrorSpace.text = ErrorMessage;
    }

    void LoggedIn()
    {
        IsLoggedIn = true;
        FirstScreen script = FirScreen.GetComponent<FirstScreen>();
        script.LoginPanelSwitch(false);
        MenuController scriptt = Header.GetComponent<MenuController>();
        scriptt.Start();
    }
}
