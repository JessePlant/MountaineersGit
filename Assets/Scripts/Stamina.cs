using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider staminaSlider;

    public float currentStamina = 100.0f;

    public float staminaRegenerationRate = 2.5f;

    public float maximumStamina = 100f;

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
            currentStamina = Mathf.Max(currentStamina, 0);
        }
        staminaSlider.value = currentStamina;
    }


    public void RegenerateStamina()
    {
        if (currentStamina < maximumStamina)
        {
            currentStamina += staminaRegenerationRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maximumStamina); 
        }
        staminaSlider.value = currentStamina;
    }
}
