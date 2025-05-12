using UnityEngine; // Provides access to Unity engine features like MonoBehaviour, Transform, Input.

/// <summary>
/// Handles player movement including walking, running, crouching, jumping, and stamina regeneration.
/// Requires a CharacterController component and should be attached to the player GameObject.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // The direction the player is moving.
    [SerializeField] Vector3 _moveDirection;

    // The reference to the CharacterController component.
    [SerializeField] CharacterController _characterController;

    // Current movement speed and movement values for walk, run, crouch, jump, and gravity.
    [SerializeField] float _movementSpeed, _walk = 5, _run = 10, _crouch = 2.5f, _jump = 8, _gravity = 20;

    // Stores horizontal (x) and vertical (y) input.
    Vector2 newInput;

    // Reference to the PlayerHandler for accessing player stats.
    PlayerHandler playerHandler;

    // Reference to UIManager for updating stamina bar.
    [SerializeField] UIManager uiManager;

    // Whether the player is currently moving.
    bool isMoving;

    // Whether the player is allowed to regenerate stamina.
    bool canRegen;

    // Timer for controlling regeneration delay.
    float regenTimer;

    #endregion
    #region Unity Callbacks

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Initializes component references.
    /// </summary>
    private void Awake()
    {
        // Gets the CharacterController component attached to the same GameObject and stores it in _characterController.
        _characterController = GetComponent<CharacterController>();
        // Gets the PlayerHandler component attached to the same GameObject and stores it in playerHandler.
        playerHandler = GetComponent<PlayerHandler>();
        // Finds the GameObject in the scene with the tag "GameManager", then gets its UIManager component and stores it in uiManager.
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }

    /// <summary>
    /// Called once per frame.
    /// Manages timer, stamina regeneration, speed changes, and movement input.
    /// </summary>
    void Update()
    {
        // Updates a timer that controls when stamina regeneration is allowed again.
        Timer();
        // Gradually regenerates the player's stamina if allowed.
        RegenOverTime();
        // Adjusts the player's movement speed based on input (walk, sprint, crouch) and stamina level.
        SpeedControls();
        // Handles player movement input and applies movement to the CharacterController.
        Move();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Regenerates the player's stamina over time if allowed.
    /// </summary>
    void RegenOverTime()
    {
        // Check if the player is allowed to regenerate stamina
        if (canRegen)
        {
            // Check if current stamina is less than the maximum allowed
            if (playerHandler.playerData.stamina.currentValue < playerHandler.playerData.stamina.maxValue)
            {
                // Increase the player's stamina over time, based on the regen rate and deltaTime
                playerHandler.playerData.stamina.currentValue += playerHandler.playerData.stamina.value * Time.deltaTime;

                // Update the stamina bar in the UI to reflect the new stamina value
                uiManager.UpdateUI(uiManager.staminaBar,
                    playerHandler.playerData.stamina.currentValue,
                    playerHandler.playerData.stamina.maxValue);
            }
        }
    }

    /// <summary>
    /// Controls the timer that delays stamina regeneration after sprinting.
    /// </summary>
    void Timer()
    {
        // Check if stamina regeneration is currently disabled
        if (!canRegen)
        {
            // Increment the regen timer by the time passed since the last frame
            regenTimer += Time.deltaTime;
            // If the regen timer has exceeded 1.5 seconds, allow stamina regeneration
            if (regenTimer >= 1.5f)
            {
                // Enable stamina regeneration
                canRegen = true;
                // Reset the regen timer
                regenTimer = 0;
            }
        }
    }

    /// <summary>
    /// Adjusts player speed based on sprint or crouch input.
    /// Also consumes stamina while sprinting.
    /// </summary>
    void SpeedControls()
    {
        // Check if the player is pressing the Sprint key, has stamina left, and is moving
        if (Input.GetKey(KeybindManager.keys["Sprint"]) &&
            playerHandler.playerData.stamina.currentValue > 0 && isMoving)
        {
            // Set the movement speed to the sprint value
            _movementSpeed = _run;
            // Decrease the player's stamina by 1 per second (scaled by deltaTime)
            playerHandler.playerData.stamina.currentValue -= 1 * Time.deltaTime;
            // Update the stamina UI to reflect the current stamina value
            uiManager.UpdateUI(uiManager.staminaBar,
                playerHandler.playerData.stamina.currentValue,
                playerHandler.playerData.stamina.maxValue);
            // Disable stamina regeneration while sprinting
            canRegen = false;
            // Reset the regen timer as the player is actively sprinting
            regenTimer = 0;
        }
        // Check if the player is pressing the Crouch key
        else if (Input.GetKey(KeybindManager.keys["Crouch"]))
        {
            // Set the movement speed to the crouch value
            _movementSpeed = _crouch;
        }
        // If no sprint or crouch keys are pressed
        else
        {
            // Set the normal walk speed
            _movementSpeed = _walk;
        }
    }

    /// <summary>
    /// Handles movement input, jumping, gravity, and applying it via CharacterController.
    /// </summary>
    void Move()
    {
        // Check if the CharacterController is properly assigned (not null)
        if (_characterController != null)
        {
            // Check if the character is grounded (on the floor or surface)
            if (_characterController.isGrounded)
            {
                // Check if the player is pressing the Left movement key
                if (Input.GetKey(KeybindManager.keys["Left"]))
                {
                    // Set the horizontal input to -1 (move left)
                    newInput.x = -1;
                    isMoving = true;
                }
                // Check if the player is pressing the Right movement key
                else if (Input.GetKey(KeybindManager.keys["Right"]))
                {
                    // Set the horizontal input to 1 (move right)
                    newInput.x = 1;
                    isMoving = true;
                }
                // No left or right key is pressed 
                else
                {
                    // Stop horizontal movement
                    newInput.x = 0;
                    isMoving = false;
                }

                // Check if the player is pressing the Backward movement key
                if (Input.GetKey(KeybindManager.keys["Backward"]))
                {
                    // Set the vertical input to -1 (move backward)
                    newInput.y = -1;
                    isMoving = true;
                }
                // Check if the player is pressing the Forward movement key
                else if (Input.GetKey(KeybindManager.keys["Forward"]))
                {
                    // Set the vertical input to 1 (move forward)
                    newInput.y = 1;
                    isMoving = true;
                }
                // No forward or backward key is pressed
                else
                {
                    // Stop vertical movement
                    newInput.y = 0;
                    isMoving = false;
                }

                // Apply movement direction based on the horizontal and vertical input
                _moveDirection = new Vector3(newInput.x, 0, newInput.y);
                // Transform movement direction relative to the player's rotation (to maintain movement direction)
                _moveDirection = transform.TransformDirection(_moveDirection);
                // Multiply by the movement speed to adjust the character's movement speed
                _moveDirection *= _movementSpeed;

                // Check if the player is pressing the Jump key
                if (Input.GetKey(KeybindManager.keys["Jump"]))
                {
                    // Apply upward force to the movement direction (make the character jump)
                    _moveDirection.y = _jump;
                }
            }

            // Apply gravity to the y-axis of the movement direction
            _moveDirection.y -= _gravity * Time.deltaTime;
            // Move the character based on the movement direction (scaled by deltaTime for frame-independent movement)
            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }

    #endregion
}