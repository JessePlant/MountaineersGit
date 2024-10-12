using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilyAbility : MonoBehaviour
{
    public float heal = 10;
    public CameraController cameraController;
    public bool isOnCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOnCooldown)
        {
            Heal();
        }
    }

    void Heal()
    {
        if (Input.GetKeyDown(KeyCode.G)) { 
         HealthManager.Ghealth += heal;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            HealthManager.Ehealth += heal;
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
