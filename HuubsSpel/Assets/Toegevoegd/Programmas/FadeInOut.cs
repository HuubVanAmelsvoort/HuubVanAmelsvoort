using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts;
using System.Collections;
using Scripts.Characters;

public class FadeInOut : MonoBehaviour
{
    public string nextLevel;
    public bool startLoadingScreen;
    public bool showStatistics;
    private static FadeInOut instance;

    public static FadeInOut Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(FadeInOut)) as FadeInOut;

            if (!instance)
                Debug.Log("There need to be at least one active Player on the scene");
        }
        return instance;
    }

    private void Proceed() {
        if (!showStatistics) {
            NextScene();
            return;
        }

        UIManager.Instance().ShowStatistics();
    }

    public void NextScene()
    {
        
        if (SaveManager.Instance() != null)
        {
            SaveManager.Instance().Save(nextLevel);
        }
            if (startLoadingScreen)
            {
                SceneManager.LoadScene("LoadingScreen");
            }
            else
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
}