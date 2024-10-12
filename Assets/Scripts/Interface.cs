using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gert;
    public GameObject Emily;
    [SerializeField]
    public List<Guns> guns = new List<Guns>();
    
    void Awake()
    {
        guns.Add(new Guns("SlingShot", 5, 2, 1, 1f));
        guns.Add(new Guns("Assault Rifle", 10, 6, 35, 5));
        guns.Add(new Guns("Lazer Rifle", 40, 15, 7, 10));
        guns.ForEach(gun => Debug.Log(gun.name));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
