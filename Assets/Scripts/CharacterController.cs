using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CameraController cc;
    public GameObject Gert, Emily;
    Vector2 gertPos, emilyPos;
    float charDif;
    bool wiDist;
    public float cooldown = 0.1f;
    public float lastTime = 0; // If we still wanna implement the cooldown
    public StaminaController staminaController;
    float movementSpeed = 1f;
    Vector3 move;
    public bool isClimbing = true;
    public GameObject bulletPrefab;
    [SerializeField] private float speed = 5f;
    // Handle the cooldown between shots in AttackController - Can simulate upgrades to different types of guns and slingshots
    // Start is called before the first frame update
    void Start()
    {
        Gert = GameObject.Find("Gert");
        //Gert.transform.position = new Vector3(0, 0.4f, 0);
        Emily = GameObject.Find("Emily");
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        //Emily.transform.position = new Vector3(2.3f, 0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        gertPos = new Vector2(Gert.transform.position.x, Gert.transform.position.y);
        emilyPos = new Vector2(Emily.transform.position.x, Emily.transform.position.y);
        charDif = Vector2.Distance(gertPos, emilyPos);

        if (Math.Abs(charDif) <= 8)
        {
            if(cc.onGert) //if (staminaController.Gert > 10)
            {
                Movement(Gert,cc.onGert);

            }
            else
            {  // if (staminaController.Emily > 10)
                Movement(Emily, cc.onGert);
            }
        }
        else 
        {
            //Let them move only if it is towards each other.
        }
            /*
            if (cc.onGert)
            {
                if (true) 
                {
                    Movement(Gert, cc.onGert);
                    //Debug.Log("Gert Moving");
                    lastTime = Time.time;
                }
            }
            else
            {
                if (true) 
                {
                    Movement(Emily, cc.onGert);
                    lastTime = Time.time;
                    //Debug.Log("Emily Moving");
                }
            }
        }
        else
        {
            if (cc.onGert)
            {
                cc.onGert = false; // Need to Clean this up but its fine for now and dev 
                Movement(Emily, cc.onGert);
                Debug.Log("Gert Moving");
                lastTime = Time.time;
            }
            else
            {
                cc.onGert = true;
                Movement(Gert, cc.onGert);
                lastTime = Time.time;
                Debug.Log("Emily Moving");
            }

        }
    
*/
    }


    public void Movement(GameObject gameObject,bool isLocked)
    {
        //if the distance between char is less than the rope.
        if(!isClimbing)
        {
            fall(isLocked, gameObject);
        }
        else
        {


            //if (Math.Abs(charDif) <= 8)
            //{
                if (Input.GetKey(KeyCode.W)) //Move up
                {
                    //Want to have this on cool down.
                    move = Vector3.up * movementSpeed * Time.deltaTime;
                    gameObject.transform.Translate(move);
                    staminaController.useStam(isLocked);
                    Debug.Log(gameObject + " is moving up.");

                }
                if (Input.GetKey(KeyCode.S)) //Move Down
                {
                    //Want to have this on cool down.
                    move = Vector3.down * movementSpeed * Time.deltaTime;
                    gameObject.transform.Translate(move);
                    staminaController.useStam(isLocked);
                    Debug.Log(gameObject + " is moving down.");

                }
                if (Input.GetKey(KeyCode.D)) //Move Right
                {
                    //Want to have this on cool down.
                    move = Vector3.right * movementSpeed * Time.deltaTime;
                    gameObject.transform.Translate(move);
                    staminaController.useStam(isLocked);
                    Debug.Log(gameObject + " is moving left.");

                }
                if (Input.GetKey(KeyCode.A)) //Move Left
                {
                    //Want to have this on cool down.
                    move = Vector3.left * movementSpeed * Time.deltaTime;
                    gameObject.transform.Translate(move);
                    staminaController.useStam(isLocked);
                    Debug.Log(gameObject + " is moving right.");
                }
            //}
//This block of code is responsible for movement.
        }
    }

    public void fall(bool whosLocked,GameObject player) // Idea: When Stam < 10 && Gets hit by a zombie then player falls
    {
        Vector3 fall = Vector3.down * 9.8f * Time.deltaTime;
        player.transform.Translate(fall);    
    }



    Vector2 SquareToCircle(Vector2 input)
    {

        return (input.sqrMagnitude >= 1f) ? input.normalized : input;
    }

}
