using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SherpaManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Guns assaultRifle;
    public Guns slingshot;
    public Guns LazerRifle;
    void Start()
    {
        assaultRifle = new Guns(10f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
