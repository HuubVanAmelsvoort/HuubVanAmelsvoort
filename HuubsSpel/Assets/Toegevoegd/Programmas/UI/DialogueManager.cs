using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
	public OptionManager ReplyManager;
	public Dialogue Dialogue;
	private Queue<string> Queue;

	[SerializeField]
	public TextMeshProUGUI Text;

	private static DialogueManager instance;

    public static DialogueManager Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(DialogueManager)) as DialogueManager;

            if (!instance)
                Debug.Log("There need to be at least one active DialogueManager on the scene");
        }

        return instance;
    }

    public void Start() {
		Queue = new Queue<string>();
		gameObject.SetActive(false);
	}

	public void DisableDialogue() {
		gameObject.SetActive(false);
		ReplyManager.DisableAnswers();
	}

	public void StartDialogue(Dialogue dialogue) {

		gameObject.SetActive(true);
		Dialogue = dialogue;
		Queue.Clear();

		foreach(var item in dialogue.ToSay)
			Queue.Enqueue(item);

		DisplayNext();
	}

	public void DisplayNext() {
		if(Queue.Count == 0)
			return;

		Text.text = Queue.Dequeue();

		if(Queue.Count != 0)
			return;
			
		try {
			ReplyManager.DisplayOptions(Dialogue);
		}
		catch(Exception) {
			gameObject.SetActive(false);
		}
	}
}