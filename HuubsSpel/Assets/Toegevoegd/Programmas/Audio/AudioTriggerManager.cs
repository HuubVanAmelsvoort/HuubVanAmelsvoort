using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static AudioTrigger;

public class AudioTriggerManager : MonoBehaviour {
	public AudioTrigger[] audioTriggers;

	private Button button;

	public void Awake() {
		button = GetComponent<Button>();

		for(int i = 0; i < audioTriggers.Length; i++) {
			AudioTrigger audioTrigger = audioTriggers[i];
			if(audioTrigger.triggerType.Equals(TriggerType.OnClick))
				button.GetComponent<Button>().onClick.AddListener(delegate { OnAction(audioTrigger); });

			if(audioTrigger.triggerType.Equals(TriggerType.OnHover)) {
				//the code below is required to add a trigger listener as a callback for hovering the mouse over an object to make OnHover() method work properly.
				//see for more info: https://forum.unity.com/threads/adding-mouse-over-event-to-toggle.463709/#post-3015047
				EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();
				EventTrigger.Entry eventEntry = new EventTrigger.Entry();
				eventEntry.eventID = EventTriggerType.PointerEnter;
				eventEntry.callback.AddListener(data => OnAction(audioTrigger));
				eventTrigger.triggers.Add(eventEntry);
			}
		}
	}

	public void OnAction(AudioTrigger at) {
		AudioManager.Instance().Play(at.audioName, 0f, at.restart, at.overlap);
	}
}