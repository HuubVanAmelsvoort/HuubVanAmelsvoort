using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

	public GameObject animatedPart;
	public float animationResetTime;
	public float timer;
	public Button loadButton;
	public TextMeshProUGUI loadText;
	
	public void Start() {
		AudioManager.Instance().StopMusic("main menu");
		AudioManager.Instance().Play("main menu");
		AudioManager.Instance().Play("main menu");
		timer = 18;
		FinalData data = SaveSystem.Load();
		if (data == null || data.currentLevel == null)
		{
			loadButton.interactable = false;
			ColorBlock colorBlock = new ColorBlock();
			colorBlock.highlightedColor = new Color(0, 0, 0);
			loadButton.colors = colorBlock;
			loadText.color = new Color32(100,100,100,255);
			//loadText.color = new Color(100, 100, 100);
		}
	}


    public void Update()
    {
		timer += Time.deltaTime;
		
		if (timer >= animationResetTime)
        {
			timer = 0;
			animatedPart.SetActive(false);
			animatedPart.SetActive(true);
		}
    }

    public void PlayButtonClick() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitButtonClick() { 
		Debug.Log("Quit!");
		Application.Quit();
	}

	public void LoadButtonClick()
    {
        SceneManager.LoadScene("LoadingScreen");
	}

	public void CredtisButton()
    {
		SceneManager.LoadScene("10-Credits");
    }
}