using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInteraction : Interactable
{
    private Character interactable;
    public new void Start()
    {
        base.Start();
        interactable = GetComponent<Character>();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();   
    }

    public override void Interact()
    {
        ((NPC)interactable).InteractWithMe();
    }
}
