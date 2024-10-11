using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public Transform Gert;      // Reference to Gert
    public Transform Emily;     // Reference to Emily

    public Camera mainCamera;   // Main camera that follows the active player
    public Camera miniMapCamera; // Mini-map camera showing the inactive player

    public float cameraDistance = 10f;     // Distance of the camera from the player
    public float minCameraHeight = 2f;     // Minimum height of the camera above the player
    public float maxCameraHeight = 10f;    // Maximum height of the camera above the player
    public float mouseSensitivity = 100f;  // Sensitivity of mouse movement for camera rotation

    private Transform activePlayer;
    private Transform inactivePlayer;
    private float currentYaw = 0f;         // Tracks the current horizontal rotation angle
    private float currentPitch = 0f;       // Tracks the current vertical rotation angle
    public float pitchMin = -40f;          // Minimum pitch angle (to prevent flipping over)
    public float pitchMax = 85f;           // Maximum pitch angle

    private Player activePlayerScript;     // Reference to the Player script on the active player
    private Player inactivePlayerScript;     // Reference to the Player script on the active player


    private Plane[] cameraFrustum;
    void Start()
    {
        SetActivePlayer(Gert); // Start with Gert
    }

    void Update()
    {
        HandlePlayerSwitching();
        UpdateCameras();
    }

    void HandlePlayerSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle between players when pressing the "Tab" key
            SetActivePlayer(activePlayer == Gert ? Emily : Gert);
        }
    }

    void SetActivePlayer(Transform player)
    {
        activePlayer = player;
        inactivePlayer = (player == Gert) ? Emily : Gert;

        // Get the Player script on the active player
        activePlayerScript = activePlayer.GetComponent<Player>();
        inactivePlayerScript = inactivePlayer.GetComponent<Player>();

        if (activePlayerScript != null)
        {
            //activePlayerScript.RotateToFaceCamera(mainCamera.transform.forward);
            activePlayerScript.InputSpace = mainCamera.transform;
            activePlayerScript.PhysicalState.PlayerCamera = mainCamera;
        }
        if (inactivePlayerScript != null)
        {
            //inactivePlayerScript.RotateToFaceCamera(miniMapCamera.transform.forward);
            inactivePlayerScript.InputSpace = miniMapCamera.transform;
            inactivePlayerScript.PhysicalState.PlayerCamera = miniMapCamera;

        }
    }

    void UpdateCameras()
    {

        // Get mouse input for horizontal and vertical rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update horizontal rotation (Yaw) based on Mouse X
        currentYaw += mouseX;

        // Update vertical rotation (Pitch) based on Mouse Y
        currentPitch -= mouseY;  // Subtract to invert the Y axis, so moving up looks down
        currentPitch = Mathf.Clamp(currentPitch, pitchMin, pitchMax);  // Clamp to prevent over-rotation

        // Calculate the new camera position based on rotation
        Vector3 offset = new Vector3(0, 0, -cameraDistance); // Distance behind the player
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);  // Rotate around both X (pitch) and Y (yaw)
        Vector3 rotatedPosition = activePlayer.position + rotation * offset;

        // Adjust the camera's height based on pitch to avoid going underground
        rotatedPosition.y = Mathf.Clamp(rotatedPosition.y, activePlayer.position.y + minCameraHeight, activePlayer.position.y + maxCameraHeight);

        // Update the main camera's position and make it look at the active player
        mainCamera.transform.position = rotatedPosition;
        mainCamera.transform.LookAt(activePlayer); // Keep the camera looking at the active player

        // Rotate the player to face the camera's forward direction
        

        // Mini-map camera stays centered on the inactive player
        miniMapCamera.transform.position = new Vector3(inactivePlayer.position.x, inactivePlayer.position.y, miniMapCamera.transform.position.z);

        // Check if inactive player is outside the main camera's view
        //Vector3 screenPoint = mainCamera.WorldToViewportPoint(inactivePlayer.position);
        //bool inactivePlayerIsVisible = screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;

        

        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        // CHECK IF A STATIONARY PLAYER CAN APPEAR ON CAMS
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        bool inactivePlayerIsVisible = GeometryUtility.TestPlanesAABB(cameraFrustum, inactivePlayer.GetComponent<Collider>().bounds);

        miniMapCamera.gameObject.SetActive(!inactivePlayerIsVisible);

        // Origin point is the position of the inactive player
        Vector3 origin = inactivePlayer.transform.position;

        // Direction is from the inactive player to the main camera
        Vector3 direction = inactivePlayer.transform.position - mainCamera.transform.position;

        // Draw the ray in the Scene view
        //Debug.DrawLine(origin, origin - direction.normalized * 100f, Color.red); // 100f is the length of the ray

        // Perform the raycast
        if (Physics.Raycast(origin, -direction.normalized, out var hit))
        {
            inactivePlayerIsVisible &= hit.collider.CompareTag("MainCamera");
        }

        // Show mini map camera if inactive player is off-screen
        miniMapCamera.gameObject.SetActive(!inactivePlayerIsVisible);


    }
}
