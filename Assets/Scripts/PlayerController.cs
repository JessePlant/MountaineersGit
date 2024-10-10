using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Data Members
    private Player activePlayer;

    private Vector2 playerMovement;
    private bool isJumpRequested;
    private bool isClimbRequested;

    private GameObject playerGamrObject;
    #endregion

    #region Properties 
    private Player Gert { get; set; }
    private Player Emily { get; set; }
 
    #endregion
    public EnemyBehaviour enemyBehaviour;
    // Start is called before the first frame update
    void Start() 
    {

        playerGamrObject = GameObject.Find("Player");
        Gert = GameObject.Find("Gert").GetComponent<Player>();
        Emily = GameObject.Find("Emily").GetComponent<Player>();
        activePlayer = Gert;
        enemyBehaviour = GameObject.Find("Enemy").GetComponent<EnemyBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        // Detect movement input in Update
        if(!enemyBehaviour.alreadyAttacked){
=======
        //// Detect movement input in Update
>>>>>>> 3e48eb5c9ba505d2b7847c3a47fd99e0c261c0ca
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Combine input for movement direction
        playerMovement = new Vector2(moveHorizontal, moveVertical);
        playerMovement = Vector2.ClampMagnitude(playerMovement, 1f);

        isJumpRequested = Input.GetButtonDown("Jump");
        isClimbRequested = Input.GetButtonDown("Climb") ? !isClimbRequested : isClimbRequested;

        // update climbing state same time
        Gert.SetClimbing(isClimbRequested);
        Emily.SetClimbing(isClimbRequested);


        if (!(Gert.State == Player.PlayerState.DEAD || Gert.State == Player.PlayerState.DEAD))
        {
            activePlayer.MovePlayer(playerMovement, isJumpRequested);
        }
        else
        {
            Destroy(Gert.gameObject);
            Destroy(Emily.gameObject);
            Destroy(playerGamrObject);
        }



        //// Detect player switching
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activePlayer = activePlayer == Gert ? Emily : Gert;
        }

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
    }

    void FixedUpdate()
    {
        // Move player based on the movement direction set in Update
        //activePlayer.Move();
    }



}
