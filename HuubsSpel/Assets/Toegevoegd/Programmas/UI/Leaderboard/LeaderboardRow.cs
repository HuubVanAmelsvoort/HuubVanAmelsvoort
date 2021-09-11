using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardRow : MonoBehaviour
{
    public TextMeshProUGUI placeField;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI scoreField;

    public void UpdateFields(string place, string name, string score)
    {
        placeField.text = place;
        nameField.text = name;
        scoreField.text = score;
    }
}
