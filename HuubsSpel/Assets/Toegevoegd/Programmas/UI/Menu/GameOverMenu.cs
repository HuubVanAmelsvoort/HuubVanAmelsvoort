using Scripts.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public static GameOverMenu instance;
    public static bool isPaused;
    public static bool isDead;
    public Animator animator; 
    public Player player;
    void Start()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }
    // used to make sure the game doesn't continue during game

    // loads the current scene
    // resumes from the beginning of level



    public void Pause()
    {
        Time.timeScale = 0f;
    }


    public void Reload()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // loads main menu(scene 0)
    public void BackToMainMenu()
    {
        Application.Quit();
    }
}
