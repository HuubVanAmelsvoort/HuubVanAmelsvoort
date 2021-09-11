using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	private static AudioManager _instance;
	[Range(0f, 1f)]
	public float volumeMaster;
	[Range(0f, 1f)]
	public float volumeMusic;
	[Range(0f, 1f)]
	public float volumeMenu;
	[Range(0f, 1f)]
	public float volumeEffects;
	public Audio[] sounds; //these are initialized in the Unity inspector on the MainMenu scene

	public delegate void volumeValueChangeDelegate(AudioVolumeSliderControl.Type type);
	public static event volumeValueChangeDelegate VolumeChangedEvent; //this is the observer that will notify external methods that are subscribed

	//default settings
	public void Start() {
		volumeMaster = .25f;
		volumeMusic = 1f;
		volumeMenu = 1f;
		volumeEffects = 1f;

		if(_instance != null) {
			Destroy(gameObject);
			return;
		}

		_instance = this;

		for(int i = 0; i < sounds.Length; i++) {
			sounds[i].source = gameObject.AddComponent<AudioSource>();
			sounds[i].source.clip = sounds[i].clip;

			sounds[i].source.volume = volumeMaster * GetControlTypeVolume(sounds[i].category) * sounds[i].volume;
			sounds[i].source.pitch = sounds[i].pitch;
			sounds[i].source.loop = sounds[i].loop;
			sounds[i].source.playOnAwake = false;
		}

		DontDestroyOnLoad(gameObject);
	}

	public static AudioManager Instance() {
		if(_instance == null) {
			Debug.LogWarning($"AudioManager is only initialized in the MainMenu Scene. You have NOT been on the MainMenu Scene yet.. visit the scene first to make correct use of the AudioManager. If in doubt, contact Roland :)");
			return new AudioManager(); //this code is necessary to stop the game from crashing when AudioManager is missing in the scene. You should NOT remove this unless you can replace it w something else to stop warning messages - Roland
		}

		return _instance;
	}

	//created static method for external methods to notify the observer
	public static void NotifyObserver(AudioVolumeSliderControl.Type type) {
		VolumeChangedEvent(type);
	}

	//default Play(name) method to play an Audio track with specified name parameter
	public void Play(string name) {
		if (_instance == null)
			return;

		Play(name, 0f, false, false);
	}

	public void Play(string name, float delay) {
		Play(name, delay, false, false);
	}

	public void Play(string name, float delay, bool restart, bool overlap) {
		//Debug.LogError($"AudioManager - {name}");

		if (_instance == null)
			return;

		Audio audio = Array.Find(sounds, s => s.name == name);

		if(audio == null) {
			Debug.LogWarning("Sound: " + name + ", is NOT found. Please check the sounds in the AudioManager object in the inspector of Unity.");
			return;
		}

		if(audio.source.isPlaying && !restart)
			return;

		if(overlap) {
			StartCoroutine(WaitToPlayOneShot(delay, audio.source));
			return;
		}

		StartCoroutine(WaitToPlay(delay, audio.source));
	}

	//info on StartCoroutine() and IEnumerator WaitToPlay() https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
	IEnumerator WaitToPlay(float waitTime, AudioSource audioSource) {
		yield return new WaitForSeconds(waitTime);
		audioSource.Play();
	}
	IEnumerator WaitToPlayOneShot(float waitTime, AudioSource audioSource) {
		yield return new WaitForSeconds(waitTime);
		audioSource.PlayOneShot(audioSource.clip);
	}

	public void StopAll() {
		if (_instance == null)
			return;

		for (int i = 0; i < sounds.Length; i++)
			if(sounds[i].source.isPlaying)
				sounds[i].source.Stop();
	}

	public void StopMusic() {
		StopAudio(Audio.Category.MUSIC, null);
	}

	public void StopEffects() {
		StopAudio(Audio.Category.SOUND_EFFECT, null);
	}

	public void StopMenu() {
		StopAudio(Audio.Category.MENU, null);
	}
	
	//overloading - stop the audio except for passed argument name
	//parameter 'except' is to exclude the specified name from being stopped and remains to keep playing
	public void StopMusic(string except) {
		StopAudio(Audio.Category.MUSIC, except);
	}

	public void StopEffects(string except) {
		StopAudio(Audio.Category.SOUND_EFFECT, except);
	}

	public void StopMenu(string except) {
		StopAudio(Audio.Category.MENU, except);
	}

	private void StopAudio(Audio.Category category, string ignoreName) {
		if (_instance == null)
			return;

		Audio[] audioCategory = Array.FindAll(sounds, s => s.category == category && s.name != ignoreName);

		for(int i = 0; i < audioCategory.Length; i++)
			if(audioCategory[i].source.isPlaying)
				audioCategory[i].source.Stop();
	}

	//this is method adjusts the volume slider accordingly to the value that have been passed on
	//This is mostly called from AudioVolumeControl.cs which is placed in slider objects
	public void AdjustVolume(float value, AudioVolumeSliderControl.Type type) {
		switch(type) {
			case AudioVolumeSliderControl.Type.MASTER:
				volumeMaster = value;
				break;
			case AudioVolumeSliderControl.Type.MUSIC:
				volumeMusic = value;
				break;
			case AudioVolumeSliderControl.Type.MENU:
				volumeMenu = value;
				break;
			case AudioVolumeSliderControl.Type.SOUND_EFFECT:
				volumeEffects = value;
				break;
			default:
				Debug.LogWarning("Reached default AudioVolumeControl.Type case");
				break;
		}

		for(int i = 0; i < sounds.Length; i++) {
			float category;

			switch(sounds[i].category) {
				case Audio.Category.MUSIC:
					category = volumeMusic;
					break;
				case Audio.Category.MENU:
					category = volumeMenu;
					break;
				case Audio.Category.SOUND_EFFECT:
					category = volumeEffects;
					break;
				default:
					category = volumeMaster;
					break;
			}

			sounds[i].source.volume = volumeMaster * category * sounds[i].volume;
		}
	}

	//returns volume based on AudioVolumeSliderControl.Type
	public float GetControlTypeVolume(AudioVolumeSliderControl.Type type) {
		switch(type) {
			case AudioVolumeSliderControl.Type.MASTER:
				return volumeMaster;
			case AudioVolumeSliderControl.Type.MUSIC:
				return volumeMusic;
			case AudioVolumeSliderControl.Type.MENU:
				return volumeMenu;
			case AudioVolumeSliderControl.Type.SOUND_EFFECT:
				return volumeEffects;
			default:
				Debug.LogWarning("Reached default AudioVolumeControl.Type case");
				return 0f;
		}
	}

	//returns volume based on AudioVolumeSliderControl.Type
	public string GetControlTypeVolumePercentage(AudioVolumeSliderControl.Type type) {
		return ((int) (GetControlTypeVolume(type) * 100)) + "%";
	}

	//returns volume based on Audio.Category
	public float GetControlTypeVolume(Audio.Category category) {
		switch(category) {
			case Audio.Category.MUSIC:
				return volumeMusic;
			case Audio.Category.MENU:
				return volumeMenu;
			case Audio.Category.SOUND_EFFECT:
				return volumeEffects;
			default:
				Debug.LogWarning("Reached default AudioVolumeControl.Type case");
				return 0f;
		}
	}
}