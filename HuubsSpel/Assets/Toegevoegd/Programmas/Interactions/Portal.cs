using Scripts;
﻿using System.Collections;
﻿using Scripts.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Portal : Interactable {
    public bool isInteractable = true;
    public bool spawningPortal = false;

    public bool portalBeingDespawned;

    new void Start() {
        
        base.Start();
        if (spawningPortal)
            gameObject.tag = "Untagged";
        if (spawningPortal)
            SaveManager.Instance().LoadScore();
        portalBeingDespawned = false;        
    }

    // Update is called once per frame
    new void Update() {
        if (spawningPortal && !portalBeingDespawned) {
            DespawnPortal();
        }
        else if (isSelected && isInteractable) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Interact();
            }
        }
    }

    public override void Interact() {
        if (!isInteractable)
            return;

        AudioManager.Instance().Play("portal close", 0f, true, true);
        GetComponent<Animator>().SetTrigger("close");
        Player.Instance().isInteracting = true;
        Player.Instance().GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        UIManager.Instance().interactionPopUp.SetActive(false);
        isInteractable = false;
    }


    //this is called from the portal-close animation
    private void Proceed()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Elevator"))
        {
            //fade in out aanroepen
            UIManager.Instance().FadeOut();
        }
        else
        {
            FadeInOut.Instance().NextScene();
        }
    }

    public void DespawnPortal() {
        GetComponent<Animator>().SetTrigger("despawn");
        AudioManager.Instance().Play("portal close", 1.25f, true, true);
        portalBeingDespawned = true;
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}