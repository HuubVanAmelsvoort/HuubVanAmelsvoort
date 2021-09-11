using Scripts.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    //public Player player;

    public static SaveManager Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType(typeof(SaveManager)) as SaveManager;

            if (!_instance)
                Debug.Log("There need to be at least one active Savemanager on the scene");
        }
        return _instance;
    }

    public void Save(string level)
    {
        SaveSystem.Save(Player.Instance(), level);
    }

    public void LoadLevelData()
    {
        Debug.LogWarning("LOADING STARTED");
        FinalData data = SaveSystem.Load();
        LoadLevel(data.currentLevel);
    }

    public void LoadScore()
    {
        Debug.LogWarning("LOADING Score STARTED");
        FinalData data = SaveSystem.Load();
        LoadPlayer(data.playerData);
    }
    //Loads all player data, applies it to the player character.
    private void LoadPlayer(PlayerData data)
    {
        Player player = Player.Instance();
        player.attributes.Level = data.level;

        player.attributes.CurrentHealth = player.attributes.MaxHealth;
        player.attributes.MaxHealth = data.maxHealth;

        player.totalStats = data.TotalStats;
    }

    private void LoadLevel(string currentLevel)
    {
        SceneManager.LoadScene(currentLevel);
    }
}