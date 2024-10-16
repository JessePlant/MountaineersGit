using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreeb : MonoBehaviour
{
    public ChangeScene changeScene;
   public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public string[] dialogueLines;
    private int currentLine = 0;
    public float typingSpeed = 0.05f;
    
    public GameObject titleSCreenButton;
    void Start()
    {
        titleSCreenButton.SetActive(false);
        dialogueText.text = ""; // Start with empty text
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        // Loop through each line of dialogue
        while (currentLine < dialogueLines.Length)
        {
            yield return StartCoroutine(TypeLine(dialogueLines[currentLine])); // Set text
            // Show the sprite

            yield return new WaitForSeconds(1.5f); // Wait for 3 seconds before showing next line

            currentLine++;
        }
        yield return new WaitForSeconds(3f);
        EndCutscene();
    }

    void EndCutscene()
    {
        dialogueText.text = ""; // Clear the text
        characterImage.enabled = false; // Hide the sprite
        titleSCreenButton.SetActive(true);
    }
     IEnumerator TypeLine(string line)
    {
        dialogueText.text = ""; // Start with an empty text field

        // Reveal each character one by one
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Wait between each character
        }
    }
}
