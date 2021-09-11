using Scripts.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour
{
    public Text secretsFound;
    public Text npcsDefeated;
    public Text answersGiven;
    public Text correctAnswers;
    public Text correctAnswersStreak;
    public Text itemsUsed;
    public Text multiplier;
    public Text perfectRun;
    public Image perfectRunImage;

    public Text totalScore;

    public Sprite correct;
    public Sprite wrong;

    public bool perfectRunBool;

    public Text secretsFoundScore;
    public Text correctAnswersScore;
    public Text itemsUsedScore;

    void Start()
    {
        perfectRunImage.sprite = wrong;
        perfectRun.text = "+0";
        multiplier.text = "x1.0";
        UpdateData();
        SavePlayerPrefs();
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("scoreNu", PlayerPrefs.GetInt("scoreNu") + Player.Instance().stats.GetScore());
        PlayerPrefs.SetInt("npcs", PlayerPrefs.GetInt("npcs") + Player.Instance().stats.npcsDefeated);
        PlayerPrefs.SetInt("secret", PlayerPrefs.GetInt("secret") + Player.Instance().stats.secretsFound);
        PlayerPrefs.SetInt("item", PlayerPrefs.GetInt("item") + Player.Instance().stats.itemsUsed);
        if (Player.Instance().stats.isPerfectRun())
        {
            PlayerPrefs.SetInt("perfect", PlayerPrefs.GetInt("perfect") + 1);
        }
        PlayerPrefs.Save();
    }

    void UpdateData()
    {
        Statistics stats = Player.Instance().stats;

        secretsFound.text = stats.secretsFound+"";
        npcsDefeated.text = stats.npcsDefeated+"";
        answersGiven.text = stats.answersGiven+"";
        correctAnswers.text = stats.correctAnswers+"";
        correctAnswersStreak.text = stats.correctAnswerStreak+"";
        itemsUsed.text = stats.itemsUsed+"";
        multiplier.text = "x" + stats.GetBonusPointsMultiplier();
        perfectRunBool = stats.isPerfectRun();

        if (perfectRunBool)
        {
            perfectRunImage.sprite = correct;
            perfectRun.text = "+" + Statistics.perfectRunPoints;
            perfectRun.color = new Color(0, 255, 0);
        }

        if(stats.itemsUsed < 1)
        {
            itemsUsed.color = new Color(0, 255, 0);
        }

        secretsFoundScore.text = "+"+stats.GetSecretsFoundScore();
        correctAnswersScore.text = "+"+stats.GetCorrectAnswersScore();
        itemsUsedScore.text = "-" + stats.GetItemsUsedScore();

        totalScore.text = stats.GetScore() + "";
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
        UIManager.Instance().fadeInOut.SetActive(true);
        FadeInOut.Instance().NextScene();
    }
}
