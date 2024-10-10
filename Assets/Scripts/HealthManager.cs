using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float Ghealth;
    public float Ehealth;
    public CameraController cameraController;
    public float dmg = 25; 
    // Start is called before the first frame update
    void Start()
    {
        Ghealth = 100;
        Ehealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Playerdmg(String playerName)
    {
        if (playerName == "Gert")
        {
            Ghealth -= dmg;
        }
        else
        {
            Ehealth -= dmg;
        }
        if (Ghealth <= 0 || Ehealth <=0)
        {
            Debug.Log("Player is dead");
        }
    }

}
