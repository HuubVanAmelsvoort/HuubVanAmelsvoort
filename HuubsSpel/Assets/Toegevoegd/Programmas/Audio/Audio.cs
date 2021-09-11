using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio {
	public string name;
	public enum Category { MUSIC, SOUND_EFFECT, MENU };
	public Category category;
	public AudioClip clip;
	[Range(0f, 1f)]
	public float volume;
	[Range(.1f, 3f)]
	public float pitch;
	public bool loop;
	[HideInInspector]
	public AudioSource source;
}