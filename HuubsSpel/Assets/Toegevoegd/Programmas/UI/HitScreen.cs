using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScreen : MonoBehaviour
{
    public void DestroyMe()
    {
        gameObject.SetActive(false);
    }
}
