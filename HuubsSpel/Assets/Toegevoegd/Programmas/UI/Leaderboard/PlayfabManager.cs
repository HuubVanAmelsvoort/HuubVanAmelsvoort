using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PlayfabManager : MonoBehaviour
{
    public InputField nameField;
    public bool onlyLeaderboard;
    public GameObject UploadPanel;
    public GameObject rowPrefab;
    public GameObject table;

    public bool dataLoaded = false;

    void Start()
    {
        dataLoaded = false;
        if (onlyLeaderboard)
        {
            UploadPanel.SetActive(false);
        }
        nameField.text = PlayerPrefs.GetString("username");
    }

    void Update()
    {
        if (!dataLoaded)
        {
            GetLeaderboard();
        }
    }
    void OnErrorGettingData(PlayFabError err)
    {
        Debug.Log("Error getting leaderboard![PlayFab]");
        dataLoaded = false;
        Debug.Log(err.GenerateErrorReport());
    }
    
    public void PublishScore()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn() || PlayerPrefs.GetInt("scoreNu") <= 0)
            return;

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "burgers_ambtenaren_demonen",
                    Value = PlayerPrefs.GetInt("scoreNu"),
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnErrorGettingData);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        GetLeaderboard();
    }

    
    public void GetLeaderboard()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
            return;

        var request = new GetLeaderboardRequest
        {
            StatisticName = "burgers_ambtenaren_demonen",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, UpdateLeaderboardGUI, OnErrorGettingData);
    }

    void UpdateLeaderboardGUI(GetLeaderboardResult result)
    {
        ClearTable();
        foreach (var item in result.Leaderboard)
        {
            GameObject row = Instantiate(rowPrefab, table.transform);
            row.GetComponent<LeaderboardRow>().UpdateFields((item.Position + 1) + "", item.DisplayName, item.StatValue + "");            
        }
        dataLoaded = true;
    }

    public void UpdatePlayerName()
    {
        if (nameField.text.Length >= 1 && nameField.text.Length <= 20 && Regex.IsMatch(nameField.text, @"^[a-zA-Z0-9_]+$"))
        {
            PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameField.text
            }, result => {
                print($"playername is now: {result.DisplayName}");
                dataLoaded = false;
                PublishScore();
                GetLeaderboard();
            }, err => print(err.GenerateErrorReport()));
        }
    }

    public void ClearTable()
    {
        foreach (Transform child in table.transform)
        {
            if (child == null)
                continue;
            Destroy(child.gameObject);
        }
    }
}
