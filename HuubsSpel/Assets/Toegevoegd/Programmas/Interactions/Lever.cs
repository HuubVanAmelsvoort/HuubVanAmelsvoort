using Scripts.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : Interactable {
    //public GameObject hiddenLayer;
    //public Sprite backgroundSprite;
    //public Sprite wallSprite;
    public List<GameObject> Rooms;

    new void Start() {
        base.Start();

        //hiddenLayer.GetComponent<Image>().sprite = backgroundSprite;

        //foreach (GameObject item in Rooms)
        //    item.GetComponent<Image>().sprite = wallSprite;
    }

    // Update is called once per frame
    new void Update() {
        base.Update();
    }

    public override void Interact() {
        if (hasInteracted)
            return;
        hasInteracted = true;
        gameObject.tag = "Untagged";
        AudioManager.Instance().Play("new lever");
        
        if (gameObject.GetComponent<Animator>())
            GetComponent<Animator>().SetTrigger("pull");
        else
            Invoke("RevealRoom", 1);

        Player.Instance().stats.secretsFound++;
        
            
        
    }

    public void RevealRoom() {
        foreach (GameObject item in Rooms)
            Destroy(item);
    }
}