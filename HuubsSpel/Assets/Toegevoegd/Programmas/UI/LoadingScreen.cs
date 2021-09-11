using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Slider progressBar;
    public Image fill;
    private FinalData data;

    public void Start()
    {
        data = SaveSystem.Load();
        StartCoroutine(waitForSeconds(5, data.currentLevel));
    }

    //commented by Roland - this code might not be used and could be redundant.
    //
    //IEnumerator startLoading()
    //{
    //    Debug.Log("start operation");
    //    yield return new WaitForEndOfFrame();
    //    //create the async operation
    //    AsyncOperation level = SceneManager.LoadSceneAsync(3);

    //    //take progress bar fill = async operation progress
    //    while (level.progress < 1)
    //    {
    //        progressBar.value = level.progress;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    //when finished load game scene
    //}

    IEnumerator waitForSeconds(int seconds, string goToScene)
    {
        float progress = 1f / seconds;
        int i = 1;
        AsyncOperation level = SceneManager.LoadSceneAsync(goToScene);
        level.allowSceneActivation = false;

        while (seconds > 0)
        {
            yield return new WaitForSeconds(1);
            progressBar.value = progress * i;

            if (progressBar.value < progressBar.maxValue / 4f)
                fill.color = new Color(255, 0, 0, 1);
            else if (progressBar.value < progressBar.maxValue / 1.5f)
                fill.color = new Color(125, 125, 0, 1);
            else
                fill.color = new Color(0, 255, 0, 1);

            seconds -= 1;
            i++;
        }

        level.allowSceneActivation = true;
    }
}
