using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject GameCanvas;
    public TextMeshProUGUI gameOverText;
    public void goToFinalMovementScene()
    {
        SceneManager.LoadScene("FinalMovement");
        Debug.Log("Play button pressed, loading new scene");
    }
    public void OpenGameWinCanvas()
    {
        //If one character makes it above 200 meters it opens the canvas in FinalMove scene, with one button saying okay. Text displays congrats.
        //TO DO: 1) Make Canvas active. 2) Set text to win.
        
        
        GameObject.Find("GameOverScreen").SetActive(true);
        GameCanvas = GameObject.Find("GameOverScreen");
        gameOverText = GameCanvas.GetComponent<TextMeshProUGUI>();
        gameOverText.text = "Congratulations on reaching the top";
        if(GameCanvas)
        {
            Debug.Log("Active");
        }
    }
    public void OpenPlayerDead()
    {
    //When player dies it changes the text on the gamewincanvas to "you died unlucky"
        GameObject.Find("GameOverScreen").SetActive(true);
        GameCanvas = GameObject.Find("GameOverScreen");
        gameOverText = GameCanvas.GetComponent<TextMeshProUGUI>();
        gameOverText.text = "You died. Better luck summiting in the next life...";
        
    }

    public void gotoCutscene()
    {
        SceneManager.LoadScene("OpeningCutscene");
        Debug.Log("Play button pressed, loading new scene");
    }
    
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
   public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
