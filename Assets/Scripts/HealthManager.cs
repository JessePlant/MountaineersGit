using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    public static float Ghealth;
    public static float Ehealth;
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

    public void Playerdmg()
    {
        Ghealth -= 25f;
        Debug.Log("Gerts Health"+Ghealth);
    }

}
