using UnityEngine;

public class CutsceneBoss : MonoBehaviour {
	public bool startedCutscene = false;
	public NPC_Monster boss;

	void Start() {
		AudioManager.Instance().StopMusic("dungeon 1");
		AudioManager.Instance().Play("dungeon 1");

		startedCutscene = false;
	}

	//detect if player enters the "Polygon Collider 2D" component defined in the Unity inspector.
	private void OnTriggerEnter2D(Collider2D collision) {
		if(!collision.CompareTag("Player") || startedCutscene)
			return;

		StartCutscene();
	}

	public void StartCutscene() {
		startedCutscene = true;
		AudioManager.Instance().StopMusic();
		AudioManager.Instance().Play("laugh");
		boss.animator.SetTrigger("laugh");
		AudioManager.Instance().Play("boss fight 1", 6f);
	}
}