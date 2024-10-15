using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public ChangeScene changeScene;
   public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public string[] dialogueLines;
    private int currentLine = 0;
    public float typingSpeed = 0.05f;

    void Start()
    {
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

            yield return new WaitForSeconds(3f); // Wait for 3 seconds before showing next line

            currentLine++;
        }
        yield return new WaitForSeconds(3f);
        EndCutscene();
    }

    void EndCutscene()
    {
        dialogueText.text = ""; // Clear the text
        characterImage.enabled = false; // Hide the sprite
        changeScene.goToFinalMovementScene();
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
//,"There stands the Great Mount Akasha, a mountain that reaches the heavens...","It is said that the mountain was home to Akasha, the most powerful being in the world, and all his treasure","Many have ventured to summit the mountain and vanished without a trace","Today, two new challengers appear but their intentions are far purer", "The mountain has claimed their loved ones...", "And they seek to reclaim them..."
