using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPlatform1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player has the "Player" tag
        {
            //Player player = other.GetComponent<Player>();
            //if (player != null)
            //{
            //    player.CanRest = true; 
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            //Player player = other.GetComponent<Player>();
            //if (player != null)
            //{
            //    player.CanRest = false;
            //}
        }
    }
}
