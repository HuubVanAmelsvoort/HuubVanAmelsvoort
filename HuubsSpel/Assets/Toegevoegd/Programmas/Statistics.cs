using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Statistics {
    public static int perfectRunPoints = 150;

    public int score = 0;
    public int secretsFound = 0;
    public int npcsDefeated = 0;
    public int answersGiven;
    public int correctAnswers;
    public int correctAnswerStreak;
    public int maxCorrectAnswerStreak = 0;
    public int itemsUsed = 0;

    public bool isPerfectRun() {
        return answersGiven == correctAnswers && itemsUsed == 0 && answersGiven > 0;
    }

    public void UpdateCorrectAnswerStreak()
    {
        correctAnswerStreak++;
        if (correctAnswerStreak > maxCorrectAnswerStreak)
        {
            maxCorrectAnswerStreak = correctAnswerStreak;
        }
        UpdateScore();
    }

    public void EndCorrectAnswerStreak()
    {
        correctAnswerStreak = 0;
        UpdateScore();
    }

    public int GetScore()
    {
        return UpdateScore();
    }

    private int UpdateScore()
    {
        score = secretsFound * 100 + correctAnswers * 75;
        
        float multiplier = GetBonusPointsMultiplier();
        score = (int) (score * multiplier);

        if (isPerfectRun())
            score += perfectRunPoints;

        score -= itemsUsed * 50;
        return score;
    }

    public float GetBonusPointsMultiplier()
    {
        return (float) (Math.Round(((float)maxCorrectAnswerStreak) / ((float)answersGiven) / 4 + 1f, 2));
    }

    public int GetSecretsFoundScore()
    {
        return secretsFound*100;
    }
    public int GetCorrectAnswersScore()
    {   
        return correctAnswers * 75;
    }
    public int GetItemsUsedScore()
    {
        return itemsUsed * 50;
    }

}
