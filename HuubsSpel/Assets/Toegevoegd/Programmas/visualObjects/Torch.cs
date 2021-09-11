using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{

    public Animator anim;
    public float timer;
    // Start is called before the first frame updatee
    void Start()
    {
        anim.speed = 0f;
        timer = Random.Range(0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            anim.speed = 1f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
