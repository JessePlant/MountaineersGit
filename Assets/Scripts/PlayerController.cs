using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Data Members
    private Player activePlayer;
    public ChangeScene cs;
    public GameObject gameOverCanvas, gert, emily;
    [Header("Movement")]
    [SerializeField] private float maxDistanceBetweenPlayers = 2.5f; // Maximum distance allowed between Emily and Gert


    private Vector2 playerMovement;
    private bool isJumpRequested;
    private bool isClimbRequested;
    //public LineRenderer chained;
    public bool gOver;
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

        cs = GetComponent<ChangeScene>();
        
        gameOverCanvas = GameObject.Find("GameOverScreen");
        gameOverCanvas.SetActive(false);
        playerGamrObject = GameObject.Find("Player");
        gert = GameObject.Find("Gert");
        Gert = gert.GetComponent<Player>();
        emily = GameObject.Find("Emily");
        Emily = emily.GetComponent<Player>();
        //chained.SetPosition(0, Gert.transform.position);
        //chained.SetPosition(1, Emily.transform.position);
        activePlayer = Gert;
        gOver = false;

    }

    // Update is called once per frame
    void Update()
    {
        //// Detect movement input in Update
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


        ///===================
        // Calculate the potential next position for the active player
    Vector3 nextPosition = activePlayer.transform.position + new Vector3(playerMovement.x, 0, playerMovement.y) * Time.deltaTime * activePlayer.maxGroundSpeed;

    // Calculate the distance between Gert and Emily if activePlayer moves
        float currentDistance = Vector3.Distance(Gert.transform.position, Emily.transform.position);
        float newDistance;

        if(activePlayer == Gert)
    {
            newDistance = Vector3.Distance(nextPosition, Emily.transform.position);
        }
    else
        {
            newDistance = Vector3.Distance(Gert.transform.position, nextPosition);
        }

        // Check if the new distance exceeds the maximum allowed distance
        if (newDistance <= maxDistanceBetweenPlayers)
        {
            print("New Distance " + newDistance + " m");
            if (!(Gert.State == Player.PlayerState.DEAD || Gert.State == Player.PlayerState.DEAD))
            {
                if (activePlayer == Gert)
                {
                    activePlayer.MovePlayer(playerMovement, isJumpRequested, gert);
                }
                else
                {
                    activePlayer.MovePlayer(playerMovement, isJumpRequested, emily);
                }

            }
        }
        else
        {
            activePlayer.StopPlayer();
            Debug.Log("Movement prevented: Distance between players would exceed the maximum allowed.");
        }

        // Update the position of the chained line between Gert and Emily
        //chained.SetPosition(0, Gert.transform.position);
        //chained.SetPosition(1, Emily.transform.position);
        /////====
       
        //chained.SetPosition(0, Gert.transform.position);
        //chained.SetPosition(1, Emily.transform.position);

        //// Detect player switching
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activePlayer = activePlayer == Gert ? Emily : Gert;
            //Gert.transform.position = new Vector3(0,203,0);
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
       
        if(Gert.transform.position.y>10 || Emily.transform.position.y>10 || gOver==true)
        {
            gOver=true;
            Debug.Log(gOver);
            gameOverCanvas.SetActive(true);
            cs.OpenGameWinCanvas();
        }
        if(Gert.State == Player.PlayerState.DEAD || Emily.State == Player.PlayerState.DEAD)
        {
            //cs.OpenPlayerDead();
            print("Dead");
        }
    }
    

    void FixedUpdate()
    {
        // Move player based on the movement direction set in Update
        //activePlayer.Move();
    }

}


