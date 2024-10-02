using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CameraController cc;
    GameObject Gert, Emily;
    Vector2 gertPos, emilyPos;
    float charDif;
    bool wiDist;
    public float cooldown = 0.1f;
    public float lastTime = 0; // If we still wanna implement the cooldown
    public StaminaController staminaController;
    float movementSpeed = 0.2f;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        Gert = GameObject.Find("Gert");
        Emily = GameObject.Find("Emily");
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

       
       if(cc.onGert)
       {
            if(staminaController.Gert > 10)
            {
                Movement(Gert, cc.onGert);
                Debug.Log("Gert Moving");
                lastTime = Time.time;
            }
       } 
       else
       {
            if(staminaController.Emily > 10)
            {
                Movement(Emily, cc.onGert);
                lastTime = Time.time;
                Debug.Log("Emily Moving");
            }
       }
    }

    public void Movement(GameObject gameObject,bool isLocked)
    {
        //if the distance between char is less than the rope.
        
        gertPos = new Vector2(Gert.transform.position.x, Gert.transform.position.y);
        emilyPos = new Vector2(Emily.transform.position.x, Emily.transform.position.y);
        charDif = Vector2.Distance(gertPos, emilyPos);
        if(Math.Abs(charDif)<=8)
        {
            if (Input.GetKey(KeyCode.W)) //Move up
            {
                //Want to have this on cool down.
                move = Vector3.up * movementSpeed * Time.deltaTime;
                gameObject.transform.Translate(move);
                staminaController.useStam(isLocked);

            }
            if (Input.GetKey(KeyCode.S)) //Move Down
            {
                //Want to have this on cool down.
                move = Vector3.down * movementSpeed * Time.deltaTime;
                gameObject.transform.Translate(move);
                staminaController.useStam(isLocked);

            }
            if (Input.GetKey(KeyCode.D)) //Move Right
            {
                //Want to have this on cool down.
                move = Vector3.right * movementSpeed * Time.deltaTime;
                gameObject.transform.Translate(move);
                staminaController.useStam(isLocked);

            }
            if (Input.GetKey(KeyCode.A)) //Move Left
            {
                //Want to have this on cool down.
                move = Vector3.left * movementSpeed * Time.deltaTime;
                gameObject.transform.Translate(move);
                staminaController.useStam(isLocked);
            }
        }//This block of code is responsible for movement.

    }
}
