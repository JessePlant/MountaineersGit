using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float Ghealth;
    public float Ehealth;
    public CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        Ghealth = 100;
        Ehealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "enemy")
        {
            PlayerDamage(10f, cameraController);
        }
    }
    public void PlayerDamage(float damage,CameraController camera)
    {
        if (camera.onGert)
        {
            Ghealth -= damage;
        }
        else
        {
            Ehealth -= damage;
        }
        if (Ghealth <= 0 || Ehealth <=0)
        {
            Debug.Log("Player is dead");
        }
    }

}
