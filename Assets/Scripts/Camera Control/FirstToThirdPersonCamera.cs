using UnityEngine;  // Importing UnityEngine to access Unity's core functionalities like MonoBehaviour, Transform.

/// <summary>
/// This script handles the switching of the camera from first-person to third-person view
/// based on zoom input (mouse scroll). It manages mouse look for rotating the player and camera,
/// and smoothly transitions between first-person and third-person camera positions and field of view.
/// </summary>
/// <remarks>
/// Attach this script to a GameObject containing a Camera component, usually the player or the camera holder.
/// </remarks>
public class FirstToThirdPersonCamera : MonoBehaviour
{
    #region Variables

    [Header("Sensitivity Settings")]
    public static float sensitivity = 15;  // Mouse sensitivity for rotating the camera.

    [Header("Rotation Clamping")]
    [SerializeField] Vector2 rotationClamp = new Vector2(-60, 60);  // Rotation limits for up and down mouse movement.

    [Header("Camera Transition Settings")]
    public float transitionSpeed = 5;  // Speed at which the camera transitions from first-person to third-person.
    public bool isLerping;  // A flag to check if the camera is transitioning (lerping).

    [Header("Zoom Settings")]
    [SerializeField] float minZoom = 0f;  // Fully first-person zoom level.
    [SerializeField] float maxZoom = 5f;  // Fully third-person zoom level.
    [SerializeField] float zoomSpeed = 2f;  // Speed at which the zoom changes.
    private float currentZoom = 0f;  // Current zoom value, starts at 0 (first-person).

    [Header("Camera Setup")]
    public Transform playerCamera;  // Reference to the player's camera.
    public Transform player;  // Reference to the player's transform for rotation.
    public Transform firstPersonSnap;  // Position where the camera should be for first-person view.
    public Transform thirdPersonSnap;  // Position where the camera should be for third-person view.
    public Transform thirdPersonParent;  // Parent transform for the third-person camera to handle its rotation.

    float tempRotation;  // Temporary variable to store the current vertical rotation of the camera.
    float verticalRotation;  // Final vertical rotation value that gets applied to the camera.

    #endregion

    #region Public Methods

    /// <summary>
    /// Handles the camera's mouse look, allowing rotation of the player and camera.
    /// </summary>
    void HandleMouseLook()
    {
        // Rotate the player around the Y-axis based on mouse X-axis movement.
        player.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);

        // Calculate vertical rotation based on mouse Y-axis movement.
        tempRotation += Input.GetAxis("Mouse Y") * sensitivity;

        // Clamp vertical rotation within defined limits.
        tempRotation = Mathf.Clamp(tempRotation, rotationClamp.x, rotationClamp.y);

        // If the mouse is inverted, adjust the vertical rotation accordingly.
        if (MouseInvertManager.IsInverted)
        {
            verticalRotation = tempRotation;  // Inverted: same as tempRotation.
        }
        else
        {
            verticalRotation = -tempRotation;  // Normal: inverse of tempRotation.
        }

        // Apply the vertical rotation to both the first-person and third-person camera transforms.
        firstPersonSnap.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        thirdPersonParent.localEulerAngles = new Vector3(verticalRotation, 0, 0);
    }

    /// <summary>
    /// Handles zooming functionality, adjusting the camera's field of view and position based on mouse scroll input.
    /// </summary>
    private void HandleZoom()
    {
        // Get the mouse scroll wheel input.
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the current zoom level based on scroll input, clamped between min and max zoom.
        currentZoom = Mathf.Clamp(currentZoom - scrollInput * zoomSpeed, minZoom, maxZoom);

        // Calculate the target position for the camera based on the zoom level, transitioning between first and third-person positions.
        Vector3 targetPosition = Vector3.Lerp(firstPersonSnap.position, thirdPersonSnap.position, currentZoom / maxZoom);

        // Smoothly move the camera to the target position using Lerp for a smooth transition.
        playerCamera.position = Vector3.Lerp(playerCamera.position, targetPosition, Time.deltaTime * transitionSpeed);

        // Adjust the camera's field of view based on the zoom level (higher zoom means a more "zoomed out" view).
        playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(40, 80, currentZoom / maxZoom);
    }

    #endregion

    #region Unity Callbacks

    /// <summary>
    /// Unity's Update method called once per frame.
    /// It updates the mouse look and zoom functionality.
    /// </summary>
    private void Update()
    {
        // Call the method to handle mouse look.
        HandleMouseLook();

        // Call the method to handle zoom functionality.
        HandleZoom();
    }

    #endregion
}
