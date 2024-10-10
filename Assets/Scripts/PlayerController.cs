using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Data Members
    public Player activePlayer;
    #endregion

    #region Properties 
    private Player Gert { get; set; }
    private Player Emily {  get; set; }
    #endregion

    // Start is called before the first frame update
    void Start() 
    { 
        Gert = GameObject.Find("Gert").GetComponent<Player>();
        Emily = GameObject.Find("Emily").GetComponent<Player>();
        activePlayer = Gert;

    }

    // Update is called once per frame
    void Update()
    {
        //// Detect movement input in Update
        //float moveVertical = Input.GetAxis("Vertical");
        //float moveHorizontal = Input.GetAxis("Horizontal");

        //// Combine input for movement direction
        //Vector2 move = new Vector2(moveHorizontal, moveVertical);

        //// Only proceed if there is movement input
        //if (move.sqrMagnitude > 0.01f)
        //{
        //    move = (move.sqrMagnitude >= 1f) ? move.normalized : move;
        //    activePlayer.SetMoveDirection(move); // Set movement direction for FixedUpdate to process
        //}
        //else
        //{
        //    activePlayer.SetMoveDirection(Vector2.zero); // Ensure player stops if no input
        //}

        //// Detect player switching
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    activePlayer = activePlayer == Gert ? Emily : Gert;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (Gert.CanRest && Emily.CanRest)
        //    {
        //        Gert.State = Player.PlayerState.RESTING;
        //        Emily.State = Player.PlayerState.RESTING;
        //    } else
        //    {
        //        Gert.State = Player.PlayerState.CLIMBING;
        //        Emily.State = Player.PlayerState.CLIMBING;
        //    }
        //}
    }

    void FixedUpdate()
    {
        // Move player based on the movement direction set in Update
        //activePlayer.Move();
    }


}
