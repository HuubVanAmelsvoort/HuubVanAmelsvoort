using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FinalData
{
    public PlayerData playerData;
    public string currentLevel;

    public FinalData(PlayerData pData, string level)
    {
        playerData = pData;
        currentLevel = level;
    }
}
