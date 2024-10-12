using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
   
    public void goToFinalMovementScene()
    {
        SceneManager.LoadScene("FinalMovement");
        Debug.Log("Play button pressed, loading new scene");
    }

    public void Quit()
    {
        //Here will close game.
    }
}
