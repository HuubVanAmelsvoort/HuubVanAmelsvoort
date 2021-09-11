using UnityEngine;

public class Gate : MonoBehaviour {
	public bool isOpen;
	public bool playerInRange;
	public BoxCollider2D box;
	public BoxCollider2D triggerArea;
	public Animator animator;

	void Start() {
		playerInRange = false;
		Unlock(false, false);
	}

	public void Lock(bool playSound)
	{
		isOpen = false;
		animator.SetTrigger("close");
		animator.ResetTrigger("open");
		gameObject.tag = "Untagged";
		box.enabled = true;

		if (playSound)
			PlaySound();
	}

	public void Unlock(bool playSound, bool openedByBoss) {
		isOpen = true;
		animator.SetTrigger("open");
		animator.ResetTrigger("close");
		box.enabled = false;
		if(playSound)
			PlaySound();

		if (openedByBoss)
			triggerArea.enabled = false;
	}

	public void PlaySound() {
		AudioManager.Instance().Play("spiketrap");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("Player") && isOpen)
			Lock(true);
	}
}