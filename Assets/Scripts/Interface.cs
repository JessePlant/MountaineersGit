using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gert;
    public GameObject Emily;
    void Start()
    {
        Gert = GameObject.Find("Gert");
        Emily = GameObject.Find("Emily");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
