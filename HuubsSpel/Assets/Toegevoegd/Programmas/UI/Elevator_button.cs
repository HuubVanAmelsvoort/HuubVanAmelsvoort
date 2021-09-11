using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator_button : MonoBehaviour
{
    public Image warningImage;
    public Color warningColor;
    public float timer;
    public bool shown;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shown)
        {
            float alpha = 0f;
            if(timer < 0.5f)
            {
                timer += Time.deltaTime;
                alpha = 1f;
            }
            else if(timer >= 0.5f && timer <= 1f)
            {
                timer += Time.deltaTime;
                alpha = 0f;
            }
            else
            {
                timer = 0f;
            }
            warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b, alpha);
        }
        else
        {
            warningImage.color = new Color(0, 0, 0, 0);
        }
    }
}
