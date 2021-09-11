using UnityEngine;
using UnityEngine.UI;

//usage instructions: drag & drop or add this as a script component in the Unity inspector to a SLIDER component and select the Type and the functionalities should be automatically working
//If in doubt, contact Roland
public class AudioVolumeSliderControl : MonoBehaviour {
	public enum Type { MASTER, MUSIC, MENU, SOUND_EFFECT };
	public Type type;
	private Slider slider;

	void Awake() {
		slider = gameObject.GetComponent<Slider>();
		slider.value = AudioManager.Instance().GetControlTypeVolume(type);
		slider.onValueChanged.AddListener(delegate { ValueChanged(); });
	}

	public void ValueChanged() {
		AudioManager.Instance().AdjustVolume(slider.value, type);
		AudioManager.NotifyObserver(type); //notify observer
	}
}
