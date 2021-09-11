using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using Scripts.Characters;
using UnityEngine;

public abstract class NPC : Character, IInteractable, IMovable
{
    public string Name = "nameless";
    public Sprite Image;
    public bool PlayerInRange { get; set; } = false;
    public Dialogue Conversation;
    public DialogueManager _dialogueManager;
    public Player NearbyPlayer { get; set; }

    public void Awake()
    {
        // _dialogueManager = FindObjectOfType<DialogueManager>();
        _dialogueManager = DialogueManager.Instance();
    }

    public bool IsInteracting { get; set; }

    public virtual void Interact()
    {
        IsInteracting = true;
    }

    public abstract void InteractWithMe();

    public void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        else if (!PlayerInRange)
        {
            IsInteracting = false;
        }
    }

    public override void Move(float x, float y)
    {
        //todo - action to walk around in a small radius
    }

    protected bool Equals(NPC other)
    {
        return base.Equals(other) && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((NPC) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (base.GetHashCode() * 397) ^ (Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0);
        }
    }
}