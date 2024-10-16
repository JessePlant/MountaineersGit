using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void goToFinalMovementScene()
    {
        SceneManager.LoadScene("FinalMovement");
        Debug.Log("Play button pressed, loading new scene");
    }
    


    public void gotoCutscene()
    {
        SceneManager.LoadScene("OpeningCutscene");
        Debug.Log("Play button pressed, loading new scene");
    }
    
    public void goToWinScene()
    {
        SceneManager.LoadScene("Win Scene");
        Debug.Log("goToWinScene called");
    }

    public void goToLoseScene()
    {
        SceneManager.LoadScene("LoseScreen");
        Debug.Log("goToLoseScene called");
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
