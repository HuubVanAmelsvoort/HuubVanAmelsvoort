using Scripts;
using Scripts.Characters;
using UnityEngine;

public class ObjectInteractable : Interactable {
	public DialogueManager dialogueManager;
	public Dialogue dialogue;

	public override void Interact() {
		//this check makes it so that the player cant be in in infinite "interacte" loop
		if(!Player.Instance().isInteracting) {
			//start the dialogue after 1.7 sec so that the first dialogue text wont be skipt over
			StartCoroutine("StartDialogue", 3);
		}
		else {
			DialogueControl.Instance().DisplayNext();
		}
	}

	private void StartDialogue() {
		DialogueControl.Instance().StartDialogue(dialogue);
	}

	new void Start() {
		base.Start();
	}

	new void Update() {
		base.Update();
	}
}
