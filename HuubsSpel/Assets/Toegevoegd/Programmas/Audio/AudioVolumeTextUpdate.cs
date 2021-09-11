using UnityEngine;

public class AudioVolumeTextUpdate : MonoBehaviour {
	public AudioVolumeSliderControl.Type type;

	void Start() {
		AudioManager.VolumeChangedEvent += onVolumeChange; //add subscription to the observer in AudioManager
		SetText();
	}

	public void onVolumeChange(AudioVolumeSliderControl.Type type) {
		if(this.type != type)
			return;
		if(this == null) {
			AudioManager.VolumeChangedEvent -= onVolumeChange; //remove subscription to the observer in AudioManager when this no longer exists
			return;
		}

		SetText();
	}

	private void SetText() {
		GetComponent<TMPro.TextMeshProUGUI>().text = AudioManager.Instance().GetControlTypeVolumePercentage(type);
	}
}
