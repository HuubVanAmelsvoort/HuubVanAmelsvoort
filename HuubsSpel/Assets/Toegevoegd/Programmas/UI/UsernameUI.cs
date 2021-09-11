using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsernameUI : MonoBehaviour
{
    public TMPro.TMP_InputField input;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            MakeCharacter();
        }
    }
    public void Back()
    {
        SceneManager.LoadScene(1);
    }
    public void MakeCharacter()
    {

        PlayerPrefs.DeleteAll();
        string username = input.text;
        PlayerPrefs.SetString("username", username);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}