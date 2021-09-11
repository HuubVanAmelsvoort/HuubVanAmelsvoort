using Scripts.Characters;
using UnityEngine;

public class Elevator_Lobby : Interactable
{
    public GameObject elevator;
    public Elevator_button button;
    public Animator animator;

    public new void Start() {
        base.Start();
        AudioManager.Instance().StopMusic("lobby loop");
        AudioManager.Instance().Play("lobby loop");
    }

    public new void Update() {
        base.Update();
    }

    public override void Interact() {
        if (hasInteracted)
            return;

        hasInteracted = true;

        button.shown = false;
        animator.SetTrigger("open");
        Player.Instance().animator.SetTrigger("walk_lift");
        Player.Instance().isInteracting = true;
        AudioManager.Instance().StopMusic();
    }

    public void CallFadeOut()
    {
        UIManager.Instance().FadeOut();
    }

    //this is being called from the elevator animator
    public void PlayElevatorOpening()
    {
        AudioManager.Instance().Play("elevator button");
        AudioManager.Instance().Play("elevator ding", .75f);
        AudioManager.Instance().Play("elevator open", 1.5f);
        AudioManager.Instance().Play("elevator music", 2.25f);
    }
}
