using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public ChangeScene changeScene;
   public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;
    private int currentLine = 0;
    public float typingSpeed = 0.05f;

    void Start()
    {
        dialogueText.text = ""; // Start with empty text
        StartCoroutine(PlayCutscene());
    }

    void Update()
    {
        SkipIntro();
    }
    public void SkipIntro()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dialogueText.text = ""; // Clear the text
            SceneManager.LoadScene("FinalMovement");
        }
    }

    IEnumerator PlayCutscene()
    {
        while (currentLine < dialogueLines.Length)
        {
            yield return StartCoroutine(TypeLine(dialogueLines[currentLine])); 
            yield return new WaitForSeconds(3f); 
            currentLine++;
        }
        yield return new WaitForSeconds(3f);
        EndCutscene();
    }

    void EndCutscene()
    {
        dialogueText.text = ""; 
        changeScene.goToFinalMovementScene();
    }
     IEnumerator TypeLine(string line)
    {
        dialogueText.text = ""; 

        // Reveal each character one by one
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); 
        }
    }
}
//,"There stands the Great Mount Akasha, a mountain that reaches the heavens...","It is said that the mountain was home to Akasha, the most powerful being in the world, and all his treasure","Many have ventured to summit the mountain and vanished without a trace","Today, two new challengers appear but their intentions are far purer", "The mountain has claimed their loved ones...", "And they seek to reclaim them..."
