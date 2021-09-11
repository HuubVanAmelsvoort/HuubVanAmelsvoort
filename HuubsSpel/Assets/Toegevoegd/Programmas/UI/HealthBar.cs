using Scripts.Characters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Character player;

    private void Start()
    {
        player = GetComponentInParent<Character>();
        slider.maxValue = player.attributes.MaxHealth;
        slider.value = player.attributes.CurrentHealth;
    }

    public void UpdateHealth(){ 
        slider.value = player.attributes.CurrentHealth;
        if (player.attributes.CurrentHealth <= player.attributes.MaxHealth / 4)
        {
            GameObject.Find("Fill").GetComponent<Image>().color = Color.red;
        }
        else if(player.attributes.CurrentHealth >= player.attributes.MaxHealth / 4 && player.attributes.CurrentHealth != player.attributes.MaxHealth)
        {
            GameObject.Find("Fill").GetComponent<Image>().color = (Color.red + Color.green);
        }else if (player.attributes.CurrentHealth == player.attributes.MaxHealth)
        {
            GameObject.Find("Fill").GetComponent<Image>().color = Color.green;
        }
    }
}