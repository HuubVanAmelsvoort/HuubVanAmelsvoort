using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public static bool isPaused;

	public void Pause() {
		Time.timeScale = 0;
		isPaused = true;
	}

	public void Resume() {
		Time.timeScale = 1f;
		isPaused = false;
	}

	public void BackToMainMenu() {
		Application.Quit();
		//SceneManager.LoadScene(0);
	}
}
