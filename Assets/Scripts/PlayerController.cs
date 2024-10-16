using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Data Members
    private Player activePlayer, inactivePlayer;
    public ChangeScene cs;
    public GameObject gameOverCanvas, gert, emily;
    [Header("Movement")]
    [SerializeField] private float maxDistanceBetweenPlayers = 2.5f; // Maximum distance allowed between Emily and Gert
    private float oldDistance = float.PositiveInfinity;
    public MountainGenerator mountainGenerator;
    public float mountainHeight = 10f;

    private Vector2 playerMovement;
    private bool isJumpRequested;
    private bool isClimbRequested;
    public bool gOver;
    #endregion

    #region Properties 
    private Player Gert { get; set; }
    private Player Emily { get; set; }
 
    #endregion
    public EnemyBehaviour enemyBehaviour;

    void Start() 
    {

        cs = GameObject.Find("SceneController").GetComponent<ChangeScene>();
        
        
        gert = GameObject.Find("Gert");
        Gert = gert.GetComponent<Player>();
        emily = GameObject.Find("Emily");
        Emily = emily.GetComponent<Player>();
        activePlayer = Gert;
        inactivePlayer = Emily;
        gOver = false;
        if (mountainGenerator)
        {
            mountainHeight = mountainGenerator.mountainHeight;
        }
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

        isJumpRequested = false;// Input.GetButtonDown("Jump");
        isClimbRequested = Input.GetButtonDown("Climb") ? !isClimbRequested : isClimbRequested;

        // update climbing state same time
        activePlayer.SetClimbing(isClimbRequested);
       
        if (!(Gert.State == Player.PlayerState.DEAD || Emily.State == Player.PlayerState.DEAD))
        {
            activePlayer.MovePlayer(playerMovement, isJumpRequested, inactivePlayer.gameObject);
        }

        //// Detect player switching
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activePlayer = activePlayer == Gert ? Emily : Gert;
            inactivePlayer = activePlayer == Gert ? Emily : Gert;
            inactivePlayer.StopPlayer();
        }

        if(Gert.transform.position.y> mountainHeight || Emily.transform.position.y> mountainHeight|| gOver==true)
        {
            Debug.Log("Win Condition met");
           cs.goToWinScene();
        }
        if (Gert.State == Player.PlayerState.DEAD || Emily.State == Player.PlayerState.DEAD || (Emily.State == Player.PlayerState.FALLING || Emily.State == Player.PlayerState.FALLING) && Mathf.Max(gert.transform.position.y, emily.transform.position.y)>5)
        {
            Debug.Log("Lose met");
            cs.goToLoseScene();
        }
    }
    

    void FixedUpdate()
    {
        // Move player based on the movement direction set in Update
        //activePlayer.Move();
    }

}


