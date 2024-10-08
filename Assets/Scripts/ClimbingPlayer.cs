using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingPlayer : MonoBehaviour
{
    public enum PlayerState
    {
        WALKING,
        FALLING,
        CLIMBING,
        RESTING
    }

    public PlayerState state = PlayerState.CLIMBING;
    float climbSpeed = 2f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // You may want to add logic here to change the state based on input or other conditions
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(moveHorizontal, moveVertical);
        move = (move.sqrMagnitude >= 1f) ? move.normalized : move;

        Transform camera = Camera.main.transform;
        Vector3 moveDirection = Quaternion.FromToRotation(camera.up, Vector3.up) * camera.TransformDirection(new Vector3(move.x, 0f, move.y));

        switch (state)
        {
            case PlayerState.FALLING:
                HandleFalling();
                break;
            case PlayerState.CLIMBING:
                HandleClimbing(move);
                break;
            case PlayerState.RESTING:
                // Ensure the player stops moving
                rb.velocity = Vector3.zero; // Stop all movement
                break;
            default:
                break;
        }

        rb.useGravity = state == PlayerState.FALLING;
    }

    void HandleFalling()
    {
        if (false && Physics.Raycast(transform.position, transform.forward * 0.4f))
        {
            state = PlayerState.CLIMBING;
        }
    }

    void HandleClimbing(Vector2 input)
    {
        Debug.Log("Player is " + state);

        Vector3 offset = transform.TransformDirection(Vector2.one * 0.5f);
        Vector3 checkDirection = Vector3.zero;
        int k = 0;

        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(transform.position + offset, transform.forward, out RaycastHit checkHit))
            {
                checkDirection += checkHit.normal;
                k++;
            }
            // Rotate Offset by 90 degrees
            offset = Quaternion.AngleAxis(90f, transform.forward) * offset;
        }

        checkDirection /= k;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -checkDirection, out hit))
        {
            float dot = Vector3.Dot(transform.forward, -hit.normal);
            rb.position = Vector3.Lerp(rb.position, hit.point + hit.normal * 0.25f, 5f * Time.fixedDeltaTime);
            transform.forward = Vector3.Lerp(transform.forward, -hit.normal, 10f * Time.fixedDeltaTime);

            rb.useGravity = false;
            rb.velocity = transform.TransformDirection(input) * climbSpeed; // Set velocity only if climbing
        }
        else
        {
            state = PlayerState.RESTING; // Set to RESTING if not climbing
        }
    }
}
