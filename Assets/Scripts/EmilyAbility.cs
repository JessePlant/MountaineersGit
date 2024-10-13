using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilyAbility : MonoBehaviour
{
    public float heal = 30;
    public CameraController cameraController;
    public bool isOnCooldown = false;
    public PhysicalState gert;
    public PhysicalState emily;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!cameraController.onGert){
        if(!isOnCooldown)
        {
            Heal();
        }
        }
    }

    void Heal()
    {
        if (Input.GetKeyDown(KeyCode.G)) { 
         gert.currentHealth += heal;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            emily.currentHealth += heal;
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(7);
        isOnCooldown = false;
    }
}
