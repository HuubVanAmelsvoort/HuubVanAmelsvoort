using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempElevatorScript : MonoBehaviour
{
    //dit heb ik tijdelijk in de '3-Elevator' scene toegevoegd zodat de AudioManager weet dat hij de elevator music moet blijven afspelen - Roland
    void Start()
    {
        AudioManager.Instance().StopMusic("elevator music");
        AudioManager.Instance().Play("elevator music");
    }
}
