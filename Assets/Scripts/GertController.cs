using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GertController : MonoBehaviour
{
    CameraController cc;
    GameObject Gert, Emily;
    Vector2 gertPos, emilyPos;
    float charDif;
    bool wiDist;
    // Start is called before the first frame update
    void Start()
    {
        Gert = GameObject.Find("Gert");
        Emily = GameObject.Find("Emily");
        cc = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
       if(cc.onGert)
       {
        Movement(Gert);
       } 
       else
       {
        Movement(Emily);
       }
        
    }

    public void Movement(GameObject gameObject)
    {
        //if the distance between char is less than the rope.
        
        gertPos = new Vector2(Gert.transform.position.x, Gert.transform.position.y);
        emilyPos = new Vector2(Emily.transform.position.x, Emily.transform.position.y);
        charDif = Vector2.Distance(gertPos, emilyPos);
        if(Math.Abs(charDif)<8)
        {
            wiDist=true;
        }
        else
        {
            wiDist = false;
        }
        //This block of code is responsible for movement.
        if(Input.GetKeyDown(KeyCode.W)&& wiDist) //Move up
        {
            //Want to have this on cool down.
            gameObject.transform.Translate(0,1,0);
            
        }
        if(Input.GetKeyDown(KeyCode.S)&& wiDist) //Move Down
        {
            //Want to have this on cool down.
            gameObject.transform.Translate(0,-1,0);
            
        }
        if(Input.GetKeyDown(KeyCode.D)&& wiDist) //Move Right
        {
            //Want to have this on cool down.
            gameObject.transform.Translate(1,0,0);
            
        }
        if(Input.GetKeyDown(KeyCode.A)) //Move Left
        {
            //Want to have this on cool down.
            gameObject.transform.Translate(-1,0,0);
            
        }
    }
}
