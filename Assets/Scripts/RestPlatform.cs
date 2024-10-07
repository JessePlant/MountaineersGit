using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthManager healthManager;
    public CharacterController characterController;
    public StaminaController staminaController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterController.isClimbing) // Need to have movement done for this
        {
            healthManager.Ghealth += 10;
            healthManager.Ehealth += 10;
            staminaController.Gert += 10; // Increment Stamina
            staminaController.Emily += 10;
        }
    }
}
