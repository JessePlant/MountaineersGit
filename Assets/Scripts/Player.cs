using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Player : MonoBehaviour
{

   public enum PlayerState
   {
        CLIMBING,
        RESTING,
        FALLING,
        DEAD
   }

    #region Data Members

    #region Serialized: Appear on unity editor
    [SerializeField] PlayerState playerState = PlayerState.CLIMBING;

    [Header("Player Input")]
    [SerializeField] Transform inputSpace = default; 

    [Header("Movement Settings")]
    [SerializeField, Range(0f, 100f)] float maxGroundSpeed = 10f, maxClimbSpeed = 4f; 
    [SerializeField, Range(0f, 100f)] float groundAcceleration = 10f, maxAirAcceleration = 1f, maxClimbAcceleration = 40f;

    [Header("Jump Settings")]
    [SerializeField, Range(0f, 10f)] float jumpHeight = 2f;
    [SerializeField, Range(0, 5)] int maxAirJumps = 0;

    [Header("Angle Settings")]
    [SerializeField, Range(0, 90)] float maxGroundAngle = 25f, maxStairsAngle = 50f;
    [SerializeField, Range(90, 170)] float maxClimbAngle = 140f;

    [Header("Snap Settings")]
    [SerializeField, Range(0f, 100f)] float maxSnapSpeed = 100f;

    [Header("Layer Mask Settings")]
    [SerializeField, Min(0f)] float probeDistance = 1f;
    [SerializeField] LayerMask probeMask = -1, stairsMask = -1, climbMask = -1;

    [Header("Materials")]
    [SerializeField] Material normalMaterial = default;
    [SerializeField] Material climbingMaterial = default;

    // New variable to track velocity before hitting the ground
    private Vector3 lastVelocity, lastPosition;

    #endregion

    #region Non-serialized: Appear on unity editor

    private Rigidbody playerRigidbody, connectedRigidbody, previousConnectedRigidbody; 
    private Vector2 movementInput;
    private Vector3 playerVelocity, connectionVelocity;
    private Vector3 connectionWorldPosition, connectionLocalPosition;

    private Vector3 upAxis, rightAxis, forwardAxis;
    private bool isJumpRequested, isClimbingRequested;

    private Vector3 contactNormal, steepNormal, climbNormal, lastClimbNormal;
    private int groundContactCount, steepContactCount, climbContactCount;

    private bool OnGround => groundContactCount > 0;
    private bool OnSteep => steepContactCount > 0;
    private bool Climbing => climbContactCount > 0 && stepsSinceLastJump > 2;

    private int jumpPhase, stepsSinceLastGrounded, stepsSinceLastJump;
    private float minGroundDotProduct, minStairsDotProduct, minClimbDotProduct;

    private PhysicalState physicalState;
    private PlayerState previousState = PlayerState.CLIMBING;

    private Animator GAnimator;
    private Animator EAnimator;
    Vector3 restingVelocity = new Vector3(0, -20, 0);
    MeshRenderer meshRenderer;
    #endregion

    #endregion


    public GameObject Gert, Emily;

    #region Properties 
    public PlayerState State
    {
        get { return playerState; }
        set { playerState = value; }
    }

    public PhysicalState PhysicalState
    {
        get { return physicalState; }
    }

    #endregion

    #region Unity Methods


    void Awake()
    {
        GAnimator = GameObject.Find("GertSprite").GetComponent<Animator>();
        EAnimator = GameObject.Find("EmilySprite").GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        physicalState = GetComponent<PhysicalState>();
        playerRigidbody.useGravity = false;
        meshRenderer = GetComponent<MeshRenderer>();
        lastPosition = lastVelocity = Vector3.zero;
        OnValidate();
        Gert = GameObject.Find("Gert");
        Emily = GameObject.Find("Emily");
    }

     private void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
        minClimbDotProduct = Mathf.Cos(maxClimbAngle * Mathf.Deg2Rad);
    }


    public void SetClimbing(bool climbing)
    {
        isClimbingRequested = climbing && playerState != PlayerState.RESTING;
    }

    public void MovePlayer(Vector2 movement, bool jump, GameObject playa)
    {
        movementInput = movement;

        if (inputSpace)
        {
            
            
            rightAxis = ProjectDirectionOnPlane(playa.transform.right, upAxis);
            forwardAxis =
                ProjectDirectionOnPlane(playa.transform.up, upAxis);
            
        }
        else
        {
            rightAxis = ProjectDirectionOnPlane(playa.transform.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(playa.transform.forward, upAxis);
            
            /*rightAxis = ProjectDirectionOnPlane(Vector3.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(Vector3.forward, upAxis);
            */
        }

        isJumpRequested |= jump;
        isClimbingRequested = Input.GetButtonDown("Climb") ? !isClimbingRequested : isClimbingRequested;

    }

    void Update()
    {
        meshRenderer.material = Climbing ? climbingMaterial : normalMaterial;
        //if (physicalState.IsOutOfStamina && !OnGround)
        //{
        //    playerState = PlayerState.FALLING;
        //}

        //if (playerState == PlayerState.FALLING && OnGround)
        //{
        //    playerState = PlayerState.DEAD;
        //}
        if (!physicalState.IsAlive)
        {
            playerState = PlayerState.DEAD;
            return;
        }

        if (physicalState.IsOutOfStamina && !OnGround)
        {
            playerState = PlayerState.FALLING;
            playerRigidbody.useGravity = true;
        }

        if (Physics.Raycast(playerRigidbody.position, Vector3.up, out var hit1, 3f) && Physics.Raycast(playerRigidbody.position, Vector3.down, out var hit2, 3f))
        {
            if (hit1.collider.CompareTag("Rest Platform") && hit2.collider.CompareTag("Rest Platform"))
            {
                previousState = playerState;
                playerState = PlayerState.RESTING;
                physicalState.RegenerateStamina();
            }
            else
            {
                playerState = previousState;
            }

        }
        else
        {
            playerState = previousState;
        }
        float Gspeed = checkMovement(Gert);
        GAnimator.speed = Gspeed;
        float Espeed = checkMovement(Emily);
        EAnimator.speed = Espeed;
    }
    float checkMovement(GameObject character)
    {
        Rigidbody characterRigidbody = character.GetComponent<Rigidbody>();
        if (characterRigidbody.velocity.magnitude < 1)
        {
            return 0;
        }
        else{
            return 1;
        }
    }

    void FixedUpdate()
    {
        Vector3 gravity = CustomGravity.GetGravity(playerRigidbody.position, out upAxis);
        lastVelocity = playerRigidbody.velocity; // Track the velocity in every frame

        UpdateState();
        AdjustVelocity();

        if (!Climbing && playerState == PlayerState.CLIMBING && OnGround)
        {
            physicalState.RegenerateStamina();
        } 

        if (isJumpRequested)
        {
            isJumpRequested = false;
            Jump(gravity);
        }

        if (Climbing)
        {
            playerVelocity -=
                contactNormal * (maxClimbAcceleration * 0.9f * Time.deltaTime);
        }
        else if (OnGround && playerVelocity.sqrMagnitude < 0.01f)
        {
            playerVelocity +=
                contactNormal *
                (Vector3.Dot(gravity, contactNormal) * Time.deltaTime);
        }
        else if (isClimbingRequested && OnGround)
        {
            playerVelocity +=
                (gravity - contactNormal * (maxClimbAcceleration * 0.9f)) *
                Time.deltaTime;
        }
        else
        {
            playerVelocity += gravity * Time.deltaTime;
        }
        playerRigidbody.velocity = playerVelocity;
       

        ClearState();

        if (movementInput != Vector2.zero && playerRigidbody.position != lastPosition)
        {
            physicalState.ConsumeStamina();
        }

        

        lastPosition = playerRigidbody.position;
        isClimbingRequested &= !physicalState.IsOutOfStamina;
    }

    void ClearState()
    {
        groundContactCount = steepContactCount = climbContactCount = 0;
        contactNormal = steepNormal = climbNormal = Vector3.zero;
        connectionVelocity = Vector3.zero;
        previousConnectedRigidbody = connectedRigidbody;
        connectedRigidbody = null;
        playerState = PlayerState.CLIMBING;
        playerRigidbody.useGravity = false;
    }

    void UpdateState()
    {
        stepsSinceLastGrounded += 1;
        stepsSinceLastJump += 1;
        playerVelocity = playerRigidbody.velocity;
        if (
            CheckClimbing() || OnGround || SnapToGround() || CheckSteepContacts()
        )
        {
            stepsSinceLastGrounded = 0;
            if (stepsSinceLastJump > 1)
            {
                jumpPhase = 0;
            }
            if (groundContactCount > 1)
            {
                contactNormal.Normalize();
            }
        }
        else
        {
            contactNormal = upAxis;
        }

        if (connectedRigidbody)
        {
            if (connectedRigidbody.isKinematic || connectedRigidbody.mass >= playerRigidbody.mass)
            {
                UpdateConnectionState();
            }
        }
    }

    void UpdateConnectionState()
    {
        if (connectedRigidbody == previousConnectedRigidbody)
        {
            Vector3 connectionMovement =
                connectedRigidbody.transform.TransformPoint(connectionLocalPosition) -
                connectionWorldPosition;
            connectionVelocity = connectionMovement / Time.deltaTime;
        }
        connectionWorldPosition = playerRigidbody.position;
        connectionLocalPosition = connectedRigidbody.transform.InverseTransformPoint(
            connectionWorldPosition
        );
    }

    bool CheckClimbing()
    {
        if (Climbing)
        {
            if (climbContactCount > 1)
            {
                climbNormal.Normalize();
                float upDot = Vector3.Dot(upAxis, climbNormal);
                if (upDot >= minGroundDotProduct)
                {
                    climbNormal = lastClimbNormal;
                }
            }
            groundContactCount = 1;
            contactNormal = climbNormal;
            return true;
        }
        return false;
    }

    bool SnapToGround()
    {
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
        {
            return false;
        }
        float speed = playerVelocity.magnitude;
        if (speed > maxSnapSpeed)
        {
            return false;
        }
        if (!Physics.Raycast(
            playerRigidbody.position, -upAxis, out RaycastHit hit,
            probeDistance, probeMask
        ))
        {
            return false;
        }

        float upDot = Vector3.Dot(upAxis, hit.normal);
        if (upDot < GetMinDot(hit.collider.gameObject.layer))
        {
            return false;
        }

        groundContactCount = 1;
        contactNormal = hit.normal;
        float dot = Vector3.Dot(playerVelocity, hit.normal);
        if (dot > 0f)
        {
            playerVelocity = (playerVelocity - hit.normal * dot).normalized * speed;
        }
        connectedRigidbody = hit.rigidbody;
        return true;
    }

    bool CheckSteepContacts()
    {
        if (steepContactCount > 1)
        {
            steepNormal.Normalize();
            float upDot = Vector3.Dot(upAxis, steepNormal);
            if (upDot >= minGroundDotProduct)
            {
                steepContactCount = 0;
                groundContactCount = 1;
                contactNormal = steepNormal;
                return true;
            }
        }
        return false;
    }

    void AdjustVelocity()
    {
        float acceleration, speed;
        Vector3 xAxis, zAxis;
        if (Climbing)
        {
            acceleration = maxClimbAcceleration;
            speed = maxClimbSpeed;
            xAxis = Vector3.Cross(contactNormal, upAxis);
            zAxis = upAxis;
        }
        else
        {
            acceleration = OnGround ? groundAcceleration : maxAirAcceleration;
            speed = OnGround && isClimbingRequested ? maxClimbSpeed : maxGroundSpeed;
            xAxis = rightAxis;
            zAxis = forwardAxis;
        }
        xAxis = ProjectDirectionOnPlane(xAxis, contactNormal);
        zAxis = ProjectDirectionOnPlane(zAxis, contactNormal);

        Vector3 relativeVelocity = playerVelocity - connectionVelocity;
        float currentX = Vector3.Dot(relativeVelocity, xAxis);
        float currentZ = Vector3.Dot(relativeVelocity, zAxis);

        float maxGroundSpeedChange = acceleration * Time.deltaTime;

        float newX =
            Mathf.MoveTowards(currentX, movementInput.x * speed, maxGroundSpeedChange);
        float newZ =
            Mathf.MoveTowards(currentZ, movementInput.y * speed, maxGroundSpeedChange);

        playerVelocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    void Jump(Vector3 gravity)
    {
        Vector3 jumpDirection;
        if (OnGround)
        {
            jumpDirection = contactNormal;
        }
        else if (OnSteep)
        {
            jumpDirection = steepNormal;
            jumpPhase = 0;
        }
        else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
        {
            if (jumpPhase == 0)
            {
                jumpPhase = 1;
            }
            jumpDirection = contactNormal;
        }
        else
        {
            return;
        }

        stepsSinceLastJump = 0;
        jumpPhase += 1;
        float jumpSpeed = Mathf.Sqrt(2f * gravity.magnitude * jumpHeight);
        jumpDirection = (jumpDirection + upAxis).normalized;
        float alignedSpeed = Vector3.Dot(playerVelocity, jumpDirection);
        if (alignedSpeed > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
        }
        playerVelocity += jumpDirection * jumpSpeed;
        playerState = PlayerState.FALLING;
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
        if (collision.collider.CompareTag("Ground") &&  lastVelocity.y < 0f)
        {
            float impactForce = lastVelocity.magnitude;
            float damageThreshold = 8f;
            if (impactForce > damageThreshold)
            {
                float damage = (impactForce - damageThreshold) * 5f; // Adjust the multiplier as needed

                // Apply damage to the player
                physicalState.Damage(damage);
            }
            playerState = PlayerState.CLIMBING;
        }

    }

    void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void EvaluateCollision(Collision collision)
    {
        int layer = collision.gameObject.layer;
        float minDot = GetMinDot(layer);
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            float upDot = Vector3.Dot(upAxis, normal);
            if (upDot >= minDot)
            {
                groundContactCount += 1;
                contactNormal += normal;
                connectedRigidbody = collision.rigidbody;
            }
            else
            {
                if (upDot > -0.01f)
                {
                    steepContactCount += 1;
                    steepNormal += normal;
                    if (groundContactCount == 0)
                    {
                        connectedRigidbody = collision.rigidbody;
                    }
                }
                if (
                    isClimbingRequested && upDot >= minClimbDotProduct &&
                    (climbMask & (1 << layer)) != 0
                )
                {
                    climbContactCount += 1;
                    climbNormal += normal;
                    lastClimbNormal = normal;
                    connectedRigidbody = collision.rigidbody;
                }
            }
        }
    }

    Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
    {
        return (direction - normal * Vector3.Dot(direction, normal)).normalized;
    }

    float GetMinDot(int layer)
    {
        return (stairsMask & (1 << layer)) == 0 ?
            minGroundDotProduct : minStairsDotProduct;
    }


    #endregion


    #region Other Methods
    #endregion
}
