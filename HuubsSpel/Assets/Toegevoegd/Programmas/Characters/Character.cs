using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IMovable {
	public Attributes attributes;
	public Animator animator;

    public void Start()
    {
        attributes.CurrentHealth = attributes.MaxHealth;
    }

    public abstract void Move(float x, float y);

	public abstract void Die();
}
