using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    // Start is called before the first frame update

    public float Gert = 100.0f;
    public float Emily = 100.0f;
    public float regenRate = 2.5f;
    public float maxStam = 100f;
    public float movementCost = 10f;
    public CameraController CameraController;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool whosLocked = CameraController.onGert;
        if( Gert < maxStam)
        {
            if ((!whosLocked) || (whosLocked && !Input.anyKeyDown)) { // Hot Fix could make it to detect if any of the necessary keys were pressed. 
                Gert += (regenRate * Time.deltaTime);
                Debug.Log("Current Gert Stamina"+Gert);
            }
        }
        if (Emily < maxStam)
        {
            if ((whosLocked) || (!whosLocked && !Input.anyKeyDown))
            {
                Emily += (regenRate * Time.deltaTime);
                Debug.Log("Current Emily Stamina" + Emily);
            }
        }
        Gert = Mathf.Clamp(Gert, 0, maxStam);
        Emily = Mathf.Clamp(Emily, 0, maxStam);
    }
    
    public void useStam(bool whosLocked)
    {
        if (whosLocked)
        {
            Gert -= movementCost * Time.deltaTime;
            Gert = Mathf.Clamp(Gert, 0, maxStam);
            Debug.Log(Gert);
            Debug.Log(Emily); 
        }
        else
        {
            Emily -= movementCost * Time.deltaTime;
            Emily = Mathf.Clamp(Emily, 0, maxStam);
        }
    }
}

