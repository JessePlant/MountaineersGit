using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider staminaSlider;

    // Current stamina level of the player
    public float currentStamina = 100.0f;

    // Rate at which stamina regenerates over time
    public float staminaRegenerationRate = 2.5f;

    // Maximum stamina the player can have
    public float maximumStamina = 100f;

    // Stamina cost associated with moving
    public float movementStaminaCost = 10f;

    void Start()
    {
        staminaSlider.maxValue = maximumStamina;
        staminaSlider.value = currentStamina;

    }

    // Consume stamina
    public void ConsumeStamina()
    {
        if (currentStamina > 0)
        {
            currentStamina -= movementStaminaCost * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0); // Ensure stamina doesn't drop below 0
        }
        staminaSlider.value = currentStamina;
    }

    // Regenerate stamina
    public void RegenerateStamina()
    {
        if (currentStamina < maximumStamina)
        {
            currentStamina += staminaRegenerationRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maximumStamina); // Ensure stamina doesn't exceed max limit
        }
        staminaSlider.value = currentStamina;
    }
}
