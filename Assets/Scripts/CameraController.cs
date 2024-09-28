using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool onGert; //If true player is controlling Gert, else controlling Emily.
    void Start()
    {
        onGert = true;
        GameObject target1, target2;
        target1 = GameObject.Find("Gert");
        target2 = GameObject.Find("Emily");
        //Set default angle to Gert.
        Camera.main.transform.position = target1.transform.position + new Vector3(0,0,-10);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            onGert = !onGert;
            //Also want to make the camera slowly move over to selected char, so the tab function should also be on a timer
        }
    }
}
