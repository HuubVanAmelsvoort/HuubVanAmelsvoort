using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class AudioTrigger {
	public enum TriggerType { OnHover, OnClick }
	public TriggerType triggerType;
	public string audioName;
	public bool restart = true;
	public bool overlap = true;
}