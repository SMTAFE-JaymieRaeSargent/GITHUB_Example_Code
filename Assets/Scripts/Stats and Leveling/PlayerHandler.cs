using UnityEngine; // Grants access to Unity’s core features like GameObjects, MonoBehaviour, Transforms, etc.
using System.Collections.Generic; // Allows use of generic collections like List and Dictionary.

/// <summary>
/// Manages the player's health, healing, and UI updates in response to game events such as damage or collision.
/// Attach this script to the Player GameObject.
/// </summary>
public class PlayerHandler : MonoBehaviour
{
    #region Variables

    // Stores the player's statistics including health, stamina, and experience
    public PlayerStats playerData = new PlayerStats();

    // Reference to the player's current spawn point in the scene
    [SerializeField] Transform spawnPoint;

    // Timer used to delay healing after taking damage
    [SerializeField] float timerValue;

    // Flag indicating whether the player is allowed to heal
    [SerializeField] bool canHeal = true;

    // Tracks previously hit tags to avoid repeated damage from the same source
    private List<string> hitTags = new List<string>();

    // Tracks currently hit tags during this frame
    private List<string> currentHitTags = new List<string>();

    // Reference to the UI manager that handles UI updates
    public UIManager uiManager;

    #endregion

    #region Public Methods

    /// <summary>
    /// Applies damage to the player and updates the health UI. Disables healing temporarily.
    /// </summary>
    /// <param name="damageValue">The amount of damage to apply to the player.</param>
    public void DamagePlayer(float damageValue)
    {
        // Reset healing timer and disable healing
        timerValue = 0;
        canHeal = false;

        // Subtract damage from the player's current health
        playerData.health.currentValue -= damageValue;

        // Update the health bar in the UI
        uiManager.UpdateUI(uiManager.healthBar, playerData.health.currentValue, playerData.health.maxValue);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Gradually heals the player over time if healing is allowed and the player's health is not full.
    /// </summary>
    void HealOverTime()
    {
        // Check if the player is allowed to regenerate health
        if (canHeal)
        {
            // Check if current health is less than the maximum allowed and the player is alive

            if (playerData.health.currentValue < playerData.health.maxValue && playerData.health.currentValue > 0)
            {
                // Increase health over time based on the health regen value
                playerData.health.currentValue += playerData.health.value * Time.deltaTime;

                // Update the health bar UI to reflect new health
                uiManager.UpdateUI(uiManager.healthBar, playerData.health.currentValue, playerData.health.maxValue);
            }
        }
    }

    /// <summary>
    /// Tracks the time since the player last took damage and re-enables healing after a delay.
    /// </summary>
    void Timer()
    {
        // Check if health regeneration is currently disabled
        if (!canHeal)
        {
            // Increment the regen timer by the time passed since the last frame
            timerValue += Time.deltaTime;

            // If the regen timer has exceeded 1.5 seconds, allow health regeneration
            if (timerValue >= 1.5f)
            {
                // Enable health regeneration
                canHeal = true;
                // Reset the regen timer
                timerValue = 0;
            }
        }
    }

    #endregion

    #region Unity Callbacks

    /// <summary>
    /// Unity's Start method. Initializes the UI with the player's starting stats.
    /// </summary>
    private void Start()
    {
        // Find and assign the UI Manager from the GameManager
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();

        // Initialize the health, stamina, and experience bars in the UI
        uiManager.UpdateUI(uiManager.healthBar, playerData.health.currentValue, playerData.health.maxValue);
        uiManager.UpdateUI(uiManager.staminaBar, playerData.stamina.currentValue, playerData.stamina.maxValue);
        uiManager.UpdateUI(uiManager.experienceBar, playerData.experience.currentValue, playerData.experience.maxValue);
    }

    /// <summary>
    /// Called when the player enters a trigger collider.
    /// Used to temporarily boost healing or update the spawn point.
    /// </summary>
    /// <param name="other">The collider the player has entered.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Heal")
        {
            // Double the healing rate while inside the Heal trigger
            playerData.health.value *= 2;
        }
        // Check if the other object the player collided with has the tag "SpawnPoint"
        if (other.tag == "SpawnPoint")
        {
            // Update the player's spawn point to the exited collider's transform
            spawnPoint = other.transform;
        }
    }

    /// <summary>
    /// Called when the player exits a trigger collider.
    /// Resets healing and updates spawn points.
    /// </summary>
    /// <param name="other">The collider the player has exited.</param>
    private void OnTriggerExit(Collider other)
    {
        // Check if the other object the player collided with has the tag "Heal"
        if (other.tag == "Heal")
        {
            // Reset the healing rate when leaving the Heal trigger
            playerData.health.value /= 2;
        }        
    }

    /// <summary>
    /// Called when the player collides with another object using a CharacterController.
    /// Applies damage on contact with damage-dealing objects.
    /// </summary>
    /// <param name="hit">The collider information from the collision.</param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Store the current tag of the object being hit in the currentHitTags list
        currentHitTags.Add(hit.gameObject.tag);
        // Check if the object being hit has the tag "Damage" and if it hasn't already been processed
        if (hit.gameObject.CompareTag("Damage") && !hitTags.Contains(hit.gameObject.tag))
        {
            // Log a message indicating that damage was hit
            Debug.Log("Hit damage");

            // Apply damage to the player by calling the DamagePlayer method with a damage value of 10
            DamagePlayer(10);
            // Add the "Damage" tag to the hitTags list to prevent repeated damage from the same object
            hitTags.Add(hit.gameObject.tag);
        }
    }

    /// <summary>
    /// Unity's LateUpdate method. Clears old collision tags to allow damage again on the next frame.
    /// </summary>
    private void LateUpdate()
    {
        // Initialize a variable to track if any current hit tags match the processed ones
        int i = 0;

        // Check if the currentHitTags list has any tags stored (i.e., check if there were any hits in the current frame)
        if (currentHitTags.Count > 0)
        {
            // Iterate through each tag in the currentHitTags list
            foreach (string tag in currentHitTags)
            {
                // If the tag is already present in the hitTags list (i.e., the tag was already processed for damage)
                if (hitTags.Contains(tag))
                {
                    // Set 'i' to 1 to indicate that a matching tag was found
                    i = 1;
                }
            }
        }

        // If no matching tags were found (i == 0)
        if (i == 0)
        {
            //  Clear the hitTags list to allow for future damage
            hitTags.Clear();
        }

        // Clear the currentHitTags list for the next frame to ensure fresh processing
        currentHitTags.Clear();
    }

    /// <summary>
    /// Unity's Update method. Handles healing over time and damage cooldown logic.
    /// </summary>
    private void Update()
    {
        // Call the HealOverTime method to potentially heal the player over time based on certain conditions
        HealOverTime();
        // Call the Timer method to update the regeneration timer and check if regeneration is allowed
        Timer();
    }

    #endregion
}