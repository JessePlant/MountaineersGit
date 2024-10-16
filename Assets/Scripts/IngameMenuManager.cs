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

    public static bool inMenu = false;  // Tracks if the game is in the menu or not
    
    // Start is called before the first frame update
    void Awake()
    {
        controlsMenu.SetActive(false);  // Hide submenus
        mainMenu.SetActive(false);  // Hide main menu
        HelpButton.SetActive(false);  // Hide any additional buttons if needed
        canvas.enabled = false;  // Ensure the Canvas is initially hidden
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
        canvas.enabled = true;  // Enable the Canvas
        mainMenu.SetActive(true);  // Show the main menu
        controlsMenu.SetActive(false);  // Ensure submenus are hidden when opening the main menu
        HelpButton.SetActive(true);  // If there are additional buttons, enable them
        Time.timeScale = 0f;  // Pause the game (optional)
    }

    // Function to close the menu
    void CloseMenu()
    {
        inMenu = false;
        mainMenu.SetActive(false);  // Hide the main menu
        controlsMenu.SetActive(false);  // Hide the controls menu
        canvas.enabled = false;  // Disable the Canvas
        Time.timeScale = 1f;  // Resume the game (optional)
    }

    // Volume control function
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);  // Set the volume
        PlayerPrefs.SetFloat("volume", volume);  // Save volume setting
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
}
