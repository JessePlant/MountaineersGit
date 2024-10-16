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
    public bool Front = true, emilyUpdated = false;
    RaycastHit Hit; 
    
    void Start()
    {
        onGert = false;
        target1 = GameObject.Find("Gert");
        target2 = GameObject.Find("Emily");
        Camera.main.orthographicSize = cameraSize;
        EmilySprite = GameObject.Find("EmilySprite");
        GertSprite = GameObject.Find("GertSprite");        
        mountMid = new Vector3 (0, 0.5f, 0);
        Camera.main.transform.position = target1.transform.position + new Vector3(0,0,-17);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            onGert = !onGert;
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
                Camera.main.transform.position = targ1Pos + Hit.normal.normalized*13; 
                target1.transform.forward = -Hit.normal;
            }
            Camera.main.transform.LookAt(targ1Pos);
            if(Hit.normal == Vector3.left || Hit.normal == Vector3.right)
            {
                Front = false;
            }
            else{
                Front = true;
            }
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
                target2.transform.forward = -Hit.normal;
            }
            Camera.main.transform.LookAt(targ2Pos);
        }

        if (!emilyUpdated)
        {
            onGert = true;
            emilyUpdated = true;
        }

    }

    void ScrollCameraUp()
    {
        Vector3 newCameraPosition = Camera.main.transform.position + new Vector3(0, scrollThreshold, 0);
        Camera.main.transform.position = Vector3.Lerp(transform.position, newCameraPosition, Time.deltaTime * scrollSpeed);
    }
}
