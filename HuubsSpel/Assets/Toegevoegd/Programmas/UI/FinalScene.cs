using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{
    public TextMeshProUGUI monstersVerslagen;
    public TextMeshProUGUI geheimenGevonden;
    public TextMeshProUGUI perfectePrestatie;
    public TextMeshProUGUI itemsUsed;
    public TextMeshProUGUI totaleScore;

    void Start()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        monstersVerslagen.text = PlayerPrefs.GetInt("npcs") + " / 22";
        geheimenGevonden.text = PlayerPrefs.GetInt("secret") + " / 11";
        perfectePrestatie.text = PlayerPrefs.GetInt("perfect") + " / 4";
        itemsUsed.text = PlayerPrefs.GetInt("item") + "";
        totaleScore.text = PlayerPrefs.GetInt("scoreNu") + " punten";
    }
    public void Next()
    {
        SceneManager.LoadScene("10-Credits");
    }
}
