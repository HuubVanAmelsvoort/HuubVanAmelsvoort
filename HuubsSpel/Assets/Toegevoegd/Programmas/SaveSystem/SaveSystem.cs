using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Scripts.Characters;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void Save(Player player, string currentLevel)
    {
        UnityEngine.Profiling.Profiler.maxUsedMemory = 768;
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.hhh";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        FinalData fData = new FinalData(data, currentLevel);
        
        formatter.Serialize(stream, fData);
        stream.Close();
    }
    public static void Delete()
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.hhh";
        File.Delete(path);
    }

    public static FinalData Load()
    {
        string path = Application.persistentDataPath + "/player.hhh";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            FinalData data = formatter.Deserialize(stream) as FinalData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}