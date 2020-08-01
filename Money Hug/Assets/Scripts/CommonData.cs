using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CommonData
{
    public int CommunityChestDonations;
    public int NumberOfUsers;
    public string CurrentWinnerDailyRaffle;
    public string CurrentWinnerCommunityChest;

    public CommonData()
    {
        CommunityChestDonations = 0;
        NumberOfUsers = 0;
        CurrentWinnerCommunityChest = "";
        CurrentWinnerDailyRaffle = "";
    }

    public CommonData(int Donation, int No, string DailyRaffleName, string CommunityChestName)
    {
        CommunityChestDonations = Donation;
        NumberOfUsers = No;
        CurrentWinnerCommunityChest = CommunityChestName;
        CurrentWinnerDailyRaffle = DailyRaffleName;
    }
}
