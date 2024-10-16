using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmilyAbility : MonoBehaviour
{
    public float heal = 30;
    public CameraController cameraController;
    public bool isOnCooldown = false;
    public PhysicalState gert;
    public PhysicalState emily;
    // Start is called before the first frame update
    public float cooldownTime = 7f;  // Total cooldown time
    private float cooldownTimer = 0f;

    public TextMeshProUGUI cooldownText;
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        cooldownText.text = "";
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
                if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;  // Reduce the cooldown timer
            cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();  // Update countdown text

            // If the cooldown is finished
            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
                cooldownText.text = "Heal Ready!!!";  // Show "Heal Ready!!!" message when cooldown ends
            }
        }
    }

    void Heal()
    {
        if (Input.GetKeyDown(KeyCode.G) && gert.currentHealth < 100) { 
            if (gert.currentHealth < 90){
                gert.currentHealth += heal;
                print("Gerts Health"+ gert.currentHealth);
                StartCoroutine(Cooldown());
            }
            else{
                gert.currentHealth = 100;
                StartCoroutine(Cooldown());
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && emily.currentHealth < 100)
        {
            if (emily.currentHealth < 90){
                emily.currentHealth += heal;
                print("Emily Health"+ emily.currentHealth);
                StartCoroutine(Cooldown());
            }
            else{
                emily.currentHealth = 100;
                StartCoroutine(Cooldown());
            };
        }
    }

    IEnumerator Cooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownTime;
        yield return new WaitForSeconds(7);
        isOnCooldown = false;
        cooldownText.text = "Heal Ready!!!";
        StartCoroutine(resetText());
    }
    IEnumerator resetText(){
        yield return new WaitForSeconds(1f);
        cooldownText.text = ""; 
    }

}
