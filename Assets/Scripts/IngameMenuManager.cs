using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class IngameMenuManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject HelpButton;
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public Image damageOverlay;  // Assign your UI Image (DamageoverlayChange) here
    private Color overlayColour;
    public float fadeSpeed = 0.3f;  // Speed at which the flash fades out
    public float flashDuration = 0.3f;  // Duration of the flash

    public Image winFlash; 
    public static bool inMenu = false;  // Tracks if the game is in the menu or not
    
    // Start is called before the first frame update
    void Awake()
    {
        controlsMenu.SetActive(false);  // Hide submenus
        mainMenu.SetActive(false);  // Hide main menu
        HelpButton.SetActive(false);  // Hide any additional buttons if needed
        canvas.enabled = false;  // Ensure the Canvas is initially hidden
        overlayColour = damageOverlay.color;
        overlayColour.a = 0;  // Fully transparent at start
        damageOverlay.color = overlayColour;
        //winFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the in-game menu visibility
            if (inMenu)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    // Function to open the menu
    void OpenMenu()
    {
        inMenu = true;
        canvas.enabled = true; 
        mainMenu.SetActive(true);  
        controlsMenu.SetActive(false);  
        HelpButton.SetActive(true);  
        Time.timeScale = 0f; 
    }

    // Function to close the menu
    void CloseMenu()
    {
        inMenu = false;
        mainMenu.SetActive(false); 
        controlsMenu.SetActive(false); 
        canvas.enabled = false;  
        Time.timeScale = 1f;  
    }

    // Volume control function
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20); 
        PlayerPrefs.SetFloat("volume", volume);  
    }

    // Show controls menu
    public void ShowControlsMenu()
    {
        mainMenu.SetActive(false);  // Hide main menu
        controlsMenu.SetActive(true);  // Show controls menu
    }

    // Go back to the main menu from the controls menu
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);  // Show main menu
        controlsMenu.SetActive(false);  // Hide controls menu
    }


    public void PlayerHitFeedback()
    {
        // Set the alpha to 0.8 (strong visibility) when hit
        overlayColour.a = 0.8f;
        damageOverlay.color = overlayColour;
        // Start the fade-out coroutine
        StartCoroutine(FadeDamageOverlay());
    }

    IEnumerator FadeDamageOverlay()
    {
        // Gradually fade out the overlay by decreasing the alpha
        while (overlayColour.a > 0)
        {
            overlayColour.a -= Time.deltaTime * fadeSpeed;  
            damageOverlay.color = overlayColour;  
            yield return null;  
        }
    }

    public void WinFlash()
    {
        winFlash.enabled = true;
       
    }

}
