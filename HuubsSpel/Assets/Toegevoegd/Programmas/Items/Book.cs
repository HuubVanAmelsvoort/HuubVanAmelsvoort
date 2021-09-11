using Scripts.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Book : MonoBehaviour
{
    public static Book instance;
    private int amount;
    public TextMeshProUGUI text;
    Player player;

    private void Awake()
    {
        player = Player.Instance();
        text.color = new Color(255, 0, 0);
    }

    public static Book Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Book)) as Book;

            if (!instance)
                Debug.Log("There need to be at least one active BOOK on the scene");
        }
        return instance;
    }

    public void Use()
    {
        if (amount <= 0)
            return;
        if (HealFull())
        {
            player.attributes.CurrentHealth = player.attributes.MaxHealth;
        }
        else
        {
            player.attributes.CurrentHealth += player.attributes.MaxHealth / 2;
            if (player.attributes.CurrentHealth > player.attributes.MaxHealth)
            {
                player.attributes.CurrentHealth = player.attributes.MaxHealth;
            }
        }
        player.healthBar.UpdateHealth();
        Player.Instance().stats.itemsUsed++;
        amount = 0;
        text.color = new Color(255,0,0);
    }

    bool HealFull()
    {
        return Random.Range(0, 2) == 1;
    }

    public void AddItem()
    {
        amount = 1;
        text.color = new Color(0, 255, 0);
    }
}
