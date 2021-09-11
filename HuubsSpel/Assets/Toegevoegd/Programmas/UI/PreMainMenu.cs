using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreMainMenu : MonoBehaviour
{
    private float timer = 0;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.01f)
        {
            SceneManager.LoadScene(1);
            Destroy(this);
        }
    }
}
