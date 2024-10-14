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

    private Vector2 playerMovement;
    private bool isJumpRequested;
    private bool isClimbRequested;
    public LineRenderer chained;
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
        chained.SetPosition(0, Gert.transform.position);
        chained.SetPosition(1, Emily.transform.position);
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


        if (!(Gert.State == Player.PlayerState.DEAD || Gert.State == Player.PlayerState.DEAD))
        {
            if(activePlayer == Gert)
            {
                activePlayer.MovePlayer(playerMovement, isJumpRequested, gert);
            }
            else
            {
                activePlayer.MovePlayer(playerMovement, isJumpRequested, emily);
            }
            
        }
        else
        {
            //Destroy(Gert.gameObject);
            //Destroy(Emily.gameObject);
            //Destroy(playerGamrObject);
        }

        chained.SetPosition(0, Gert.transform.position);
        chained.SetPosition(1, Emily.transform.position);

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
       
        if(Gert.transform.position.y>200 || Emily.transform.position.y>200 || gOver==true)
        {
            gOver=true;
            Debug.Log(gOver);
            gameOverCanvas.SetActive(true);
            cs.OpenGameWinCanvas();
        }
        if(Gert.State == Player.PlayerState.DEAD || Emily.State == Player.PlayerState.DEAD)
        {
            cs.OpenPlayerDead();
        }
    }
    

    void FixedUpdate()
    {
        // Move player based on the movement direction set in Update
        //activePlayer.Move();
    }

}


