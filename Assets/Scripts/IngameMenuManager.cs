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
    public Image damageOverlay;
    private Color overlayColour;
    public float fadeSpeed = 0.3f; 
    public float flashDuration = 0.3f; 

    public Image winFlash; 
    public static bool inMenu = false; 
    
    // Start is called before the first frame update
    void Awake()
    {
        controlsMenu.SetActive(false);  
        mainMenu.SetActive(false); 
        HelpButton.SetActive(false); 
        canvas.enabled = false;  
        overlayColour = damageOverlay.color;
        overlayColour.a = 0;  
        damageOverlay.color = overlayColour;
        //winFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    // Volume control function maybe
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20); 
        PlayerPrefs.SetFloat("volume", volume);  
    }

    // Show controls menu
    public void ShowControlsMenu()
    {
        mainMenu.SetActive(false); 
        controlsMenu.SetActive(true);  
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);  
        controlsMenu.SetActive(false);  
    }


    public void PlayerHitFeedback()
    {
        overlayColour.a = 0.8f;
        damageOverlay.color = overlayColour;
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
