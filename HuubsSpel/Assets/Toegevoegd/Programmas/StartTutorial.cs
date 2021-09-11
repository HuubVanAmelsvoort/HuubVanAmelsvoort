using Scripts;
using UnityEngine;

public class StartTutorial : MonoBehaviour {
    public Dialogue dialogue;
    public Elevator_button button;
    public bool startTutorial;
    public Sprite Image;
    public string Name;
    public Animator animator;

    void Start() {
        startTutorial = true;
        DialogueControl.Instance().StartDialogue(dialogue, false, Image, Name);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && startTutorial && !DialogueControl.Instance().IsDisabled) {
            DialogueControl.Instance().DisplayNext();
        }
        if (startTutorial && DialogueControl.Instance().IsDisabled) {
            startTutorial = false;
            if(animator !=null)
                animator.SetTrigger("fade_out");
            if(button!= null)
            {
                button.shown = true;
            }
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}