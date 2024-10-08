using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { FALLING, CLIMBING, RESTING }

    #region Data Members
    public PlayerState state = PlayerState.CLIMBING;
    public float climbSpeed = 1.0f;

    private Stamina stamina;
    private Vector2 moveDirection = Vector2.zero; // Store movement direction

    

    // Stamina 

    private Rigidbody rb;

    #endregion

    #region Properties
    public PlayerState State { 
        get { return state; } 
        set {
            if (value == PlayerState.RESTING && CanRest)
            {
                state = value;
            } else if (value != PlayerState.RESTING)
            {
                state = value;
            }
        }
    }
    public bool CanRest { get; set; }
    #endregion

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stamina = GetComponent<Stamina>();
        CanRest = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == PlayerState.RESTING)
        {
            rb.velocity = Vector3.zero;
            stamina.RegenerateStamina();
        }

    }

   

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction; // Set direction for FixedUpdate to use
    }

    public void Move()
    {
        Debug.Log("Player is " + State);
        // Only move if there's input and no surface below
        if (moveDirection != Vector2.zero && State == PlayerState.CLIMBING)
        {
            stamina.ConsumeStamina();
            // Use the previously set moveDirection to move
            Vector3 offset = transform.TransformDirection(Vector2.one * 0.5f);
            Vector3 checkDirection = Vector3.zero;
            int k = 0;

            // Raycasting logic for climbing or movement
            for (int i = 0; i < 4; i++)
            {
                if (Physics.Raycast(transform.position + offset, transform.forward, out RaycastHit checkHit))
                {
                    checkDirection += checkHit.normal;
                    k++;
                }
                offset = Quaternion.AngleAxis(90f, transform.forward) * offset;
            }

            if (k > 0) { checkDirection /= k; }

            // Perform raycast to find the climbing surface
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -checkDirection, out hit))
            {
                // Move and rotate the player towards the hit normal (climbable surface)
                rb.position = Vector3.Lerp(rb.position, hit.point + hit.normal * 0.25f, 5f * Time.fixedDeltaTime);
                transform.forward = Vector3.Lerp(transform.forward, -hit.normal, 10f * Time.fixedDeltaTime);

                rb.useGravity = false;

                // Set velocity based on input
                rb.velocity = transform.TransformDirection(moveDirection) * climbSpeed; // Apply velocity
            }
        }
        else
        {
            // Stop moving if no input
            rb.velocity = Vector3.zero;
            //state = PlayerState.RESTING;
            //rb.useGravity = true; // Optionally, re-enable gravity
        }
    }
}
