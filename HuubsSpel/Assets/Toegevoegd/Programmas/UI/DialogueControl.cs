using Scripts.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    [Serializable]
    public class DialogueControl : MonoBehaviour {
        private static DialogueControl _instance;
        public Button nextButton;
        public TextMeshProUGUI dialoguetext;
        public Player player;
        public GameObject Portait;
        public TextMeshProUGUI NamePlate;
        public Image portraitImage;
        public Queue<string> Sentences = new Queue<string>();
        public bool IsDisabled;
        public string currentNPCName;
        public Sprite currentNPCSprite;

        public static DialogueControl Instance() {
            if (!_instance) {
                UIManager.Instance().dialoguePanel.gameObject.SetActive(true);
                _instance = FindObjectOfType(typeof(DialogueControl)) as DialogueControl;
                if (!_instance) {
                    print("At least one DialogueControl needs to be active in the scene");
                    return null;
                }
            }

            return _instance;
        }

        public void StartDialogue(Dialogue dialogue, bool sound = true, Sprite image = null, string name = null) {
            Sentences.Clear();
            currentNPCName = name;
            currentNPCSprite = image;
            IsDisabled = false;
            Player.Instance().isInteracting = true;
            UIManager.Instance().dialoguePanel.SetActive(true);
            
            ChangePortrait(image, name);
                        
            foreach (var item in dialogue.ToSay) {
                Sentences.Enqueue(item);
            }


            DisplayNext(sound);
        }

        public void DisplayNext() {
            DisplayNext(true);
        }

        public void DisplayNext(bool sound) {
            if(sound)
                AudioManager.Instance().Play("onclick", 0f, true, true);

            if (Sentences.Count != 0) {
                string sentence = Sentences.Dequeue();

                if (sentence.Contains("W,A,S,D"))
                    Player.Instance().isInteracting = false;

                if (sentence[0] == '$') {
                    sentence = sentence.Substring(1);
                    ChangePortrait(Player.Instance().playerPortait, PlayerPrefs.GetString("username"));
                } else if (sentence[0] == '*') {
                    ChangePortrait(null, null);
                }
                else {
                    
                    ChangePortrait(currentNPCSprite, currentNPCName);
                }

                dialoguetext.text = sentence;
            }
            else {
                EndDialogue();
            }
        }

        public void EndDialogue() {
            Player.Instance().isInteracting = false;
            IsDisabled = true;
            UIManager.Instance().dialoguePanel.SetActive(false);
        }

        public void ChangePortrait(Sprite image, String name) {
            if (image != null && name != null)
            {
                Portait.SetActive(true);
                NamePlate.text = name;
                portraitImage.sprite = image;
            }else
                Portait.SetActive(false);
        }
    }
}