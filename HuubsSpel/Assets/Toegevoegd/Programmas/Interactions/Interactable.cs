using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isSelected = false;
    public bool hasInteracted = false;
    public void Start()
    {
        gameObject.tag = "interactable";
    }
    public abstract void Interact();

    public void Update()
    {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public void OnSelected()
    {
        isSelected = true;
    }

    public void OnDeselect()
    {
        isSelected = false;
    }
}
