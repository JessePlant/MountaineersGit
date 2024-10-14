using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool onGert; //If true player is controlling Gert, else controlling Emily.
    public float screenTopY;
    public GameObject EmilySprite, GertSprite;
    public float scrollThreshold = 1.0f;
    public float scrollSpeed = 5.0f;
     public float smoothSpeed = 0.125f;
     public float cameraSize = 4.5f;
    [SerializeField]
    public GameObject target1, target2;

    public Vector3 mountMid, targ1Pos, targ2Pos;
    RaycastHit Hit; 
    void Start()
    {
        onGert = true;
        target1 = GameObject.Find("Gert");
        target2 = GameObject.Find("Emily");
        //Set default angle to Gert.
        Camera.main.orthographicSize = cameraSize;
        EmilySprite = GameObject.Find("EmilySprite");
        GertSprite = GameObject.Find("GertSprite");
        //Here some logicc to find centre of mountain. 
        
        mountMid = new Vector3 (0, 0.5f, 0);
        Camera.main.transform.position = target1.transform.position + new Vector3(0,0,-17); //NEEDTO redo this with the current wall surface normal force direction times5. ie opposite direction player facing.
        //screenTopY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            onGert = !onGert;
            
            //Also want to make the camera slowly move over to selected char, so the tab function should also be on a timer
        }
        EmilySprite.transform.LookAt(target2.transform.position);
        GertSprite.transform.LookAt(target1.transform.position);
        if(onGert)
        {
            targ1Pos = target1.transform.position;
            mountMid = new Vector3(0, targ1Pos.y, 0);
            Vector3 Dir = mountMid-targ1Pos;
            if (Physics.Raycast(target1.transform.position, Dir, out Hit ))
            {
                GertSprite.transform.position = targ1Pos + Hit.normal.normalized*0.1f;
                Camera.main.transform.position = targ1Pos + Hit.normal.normalized*13; //Hit.normal is direction we need camera to move from player.
            }
            Camera.main.transform.LookAt(targ1Pos);
            //Camera.main.transform.position = target1.transform.position + new Vector3(0,0,-17); //NEEDTO redo this as above.
        }
        else
        {
            targ2Pos = target2.transform.position;
            mountMid = new Vector3(0, targ2Pos.y, 0);
            Vector3 Dir = mountMid-targ2Pos;
            if (Physics.Raycast(target2.transform.position, Dir, out Hit ))
            {
                EmilySprite.transform.position = targ2Pos + Hit.normal.normalized*0.1f;
                Camera.main.transform.position = targ2Pos + Hit.normal*13;
            }
            Camera.main.transform.LookAt(targ2Pos);
            //Camera.main.transform.position = target2.transform.position + new Vector3(0,0,-17);
        }
        //if(target1.transform.position.y >= screenTopY || target2.transform.position.y >= screenTopY)
        //{
            // ScrollCameraUp();
            //screenTopY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        //}

    }
    void ScrollCameraUp()
    {
        // Move the camera upwards smoothly
        Vector3 newCameraPosition = Camera.main.transform.position + new Vector3(0, scrollThreshold, 0);
        Camera.main.transform.position = Vector3.Lerp(transform.position, newCameraPosition, Time.deltaTime * scrollSpeed);
    }
}
