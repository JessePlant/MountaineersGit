using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalState : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;

    [Header("Stamina Bar")]
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Vector3 staminaBarOffset;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Vector3 healthBarOffset;

    [Header("Stamina Values")]
    [SerializeField] public float currentStamina = 100f;
    [SerializeField] private float staminaRegenerationRate = 10f;
    [SerializeField] private float maximumStamina = 100f;
    [SerializeField] private float movementStaminaCost = 10f;

    [Header("Health Values")]
    [SerializeField] public float currentHealth = 100f;
    [SerializeField] private float healthRegenerationRate = 10f;
    [SerializeField] private float maximumHealth = 100f;
    [SerializeField] private float healthDamage = 10f;
    [SerializeField] private CameraController cameraController;


    public bool IsAlive => currentHealth > 0;
    public bool IsOutOfStamina => currentStamina == 0;

    // Start is called before the first frame update
    void Start()
    {
        staminaBar.maxValue = maximumStamina;
        staminaBar.value = currentStamina;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = target.position + healthBarOffset; 
        staminaBar.transform.position = target.position + staminaBarOffset;

        healthBar.transform.rotation = camera.transform.rotation;
        staminaBar.transform.rotation = camera.transform.rotation;
        staminaBar.value = currentStamina;
        healthBar.value = currentHealth;
    }

    public void ConsumeStamina()
    {
        ConsumeStamina(movementStaminaCost);
    }

    public void ConsumeStamina(float stamina)
    {
        if (currentStamina > 0)
        {
            currentStamina -= stamina * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0); // Ensure stamina doesn't drop below 0
        }
        staminaBar.value = currentStamina;
    }

    public void RegenerateStamina()
    {
        RegenerateStamina(staminaRegenerationRate);
    }

    public void RegenerateStamina(float stamina)
    {
        if (currentStamina < maximumStamina)
        {
            currentStamina += stamina * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maximumStamina); // Ensure stamina doesn't exceed max limit
        }
        healthBar.value = currentHealth;
    }

    public void Damage()
    {
        Damage(healthDamage);
    }

    public void Damage(float health)
    {
        if (currentHealth > 0)
        {
            currentHealth -= health; // Directly apply the damage
            currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't drop below 0
        }
    }

    public void Heal()
    {
        Heal(healthRegenerationRate);
    }

    public void Heal(float health)
    {
        if (currentHealth < maximumHealth)
        {
            currentHealth += health * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maximumHealth); // Ensure stamina doesn't exceed max limit
        }
        healthBar.value = currentHealth;
    }

        public void RegenStamina(CameraController cameraController)
    {
        GameObject Gert = GameObject.Find("Gert");

        bool whosLocked = Gert.Equals(gameObject) ? true : false;
        if( currentStamina < maximumStamina)
        {
           if (whosLocked){
                if(!cameraController.onGert || (cameraController.onGert && !Input.anyKeyDown)){
                    currentStamina += staminaRegenerationRate * Time.deltaTime;
                    currentStamina = Mathf.Min(currentStamina, maximumStamina);
                    staminaBar.value = currentStamina;
            }
        }
            else{
                if(cameraController.onGert || (!cameraController.onGert && !Input.anyKeyDown)){
                    currentStamina += staminaRegenerationRate * Time.deltaTime;
                    currentStamina = Mathf.Min(currentStamina, maximumStamina);
                    staminaBar.value = currentStamina;
                }
            }
        }
    }

}
