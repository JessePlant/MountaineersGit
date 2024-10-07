using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingPlayer : MonoBehaviour
{
    public enum PlayerState
    {
        WALKING,
        FALLING,
        CLIMBING
    }
    public PlayerState state = PlayerState.CLIMBING;
    float walkSpeed = 3f;
    float climbSpeed = 2f;
    public float rotationSpeed = 120.0f; // Set player's rotation speed.


    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Debug.Log("Player is " + state);

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(moveHorizontal, moveVertical);
        move = (move.sqrMagnitude >= 1f) ? move.normalized : move;

        Transform camera = Camera.main.transform;
        Vector3 moveDirection = Quaternion.FromToRotation(camera.up, Vector3.up) * camera.TransformDirection(new Vector3(move.x, 0f, move.y));

        switch (state)
        {
            case PlayerState.WALKING:
                HandleWalking(moveDirection);
                break;
            case PlayerState.FALLING:
                HandleFalling();
                break;
            case PlayerState.CLIMBING:
                HandleClimbing(move);
                break;
            default:
                break;
        }
        rb.useGravity = state != PlayerState.CLIMBING;
    }

    void HandleWalking(Vector3 moveDirection)
    {
        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * walkSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void HandleFalling()
    {
        if (/*jumpDown &&*/ false &&  Physics.Raycast(transform.position,
                                        transform.forward * 0.4f))
            state = PlayerState.CLIMBING;
    }

    void HandleClimbing(Vector2 input)
    {
        // Check walls in a cross pattern
        Vector3 offset = transform.TransformDirection(Vector2.one * 0.5f);
        Vector3 checkDirection = Vector3.zero;
        int k = 0;
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(transform.position + offset,
                                transform.forward,
                                out RaycastHit checkHit))
            {
                checkDirection += checkHit.normal;
                k++;
            }
            // Rotate Offset by 90 degrees
            offset = Quaternion.AngleAxis(90f, transform.forward) * offset;
        }
        checkDirection /= k;

        // Check wall directly in front
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -checkDirection, out hit))
        {
            float dot = Vector3.Dot(transform.forward, -hit.normal);

            // Move slight away from the wall by 0.25f * hit.normal
            rb.position = Vector3.Lerp(rb.position, hit.point + hit.normal * 0.25f, 5f * Time.fixedDeltaTime);
            transform.forward = Vector3.Lerp(transform.forward, -hit.normal, 10f * Time.fixedDeltaTime);

            rb.useGravity = false;
            rb.velocity = transform.TransformDirection(input) * climbSpeed;
            //if (jumpDown)
            //{
            //    rb.velocity = Vector3.up * 5f + hit.normal * 2f;
            //    state = PlayerState.FALLING;
            //}
        }
        else
        {
            state = PlayerState.FALLING;
        }
    }
}