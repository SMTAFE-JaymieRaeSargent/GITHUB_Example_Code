using UnityEngine;// Grants access to Unity’s core features, such as manipulating GameObjects and handling input.
using UnityEngine.UI;// UnityEngine.UI namespace provides access to Unity's UI components like Dropdown and Text.
/// <summary>
/// Manages the game's quality settings.
/// Attach this script to a UI Dropdown or Button to allow players to select a quality level.
/// </summary>
public class QualityManager : MonoBehaviour
{
    #region Variables

    // Reference to the UI Dropdown component that will allow the player to select a quality level
    [SerializeField] Dropdown _qualityDropdown;

    // Private variable to track the current quality level index
    int _currentqualityIndex = 0;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the index of the current quality level.
    /// This index corresponds to the quality levels defined in the Unity Editor under Edit > Project Settings > Quality.
    /// </summary>
    public int CurrentQualityIndex
    {
        set { _currentqualityIndex = value; }
        get { return _currentqualityIndex; }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Changes the game's quality level based on the index provided.
    /// Quality levels are predefined in the Unity editor (located in Edit > Project Settings > Quality).
    /// This method is called when the player selects a new quality level from the dropdown.
    /// </summary>
    /// <param name="qualityIndex">The index of the quality level to set. 0 is the lowest quality, higher values correspond to higher quality settings.</param>
    public void ChangeQuality(int qualityIndex)
    {
        // Update the current quality index with the new value selected by the player
        CurrentQualityIndex = qualityIndex;

        // Set the new quality level in the game based on the index provided (the index corresponds to Unity's Quality settings)
        QualitySettings.SetQualityLevel(CurrentQualityIndex);
    }

    #endregion

    #region Unity Callbacks

    /// <summary>
    /// Unity Start method is called before the first frame update.
    /// Initializes the quality dropdown with the current quality level and applies it to the game.
    /// </summary>
    private void Start()
    {
        // Set the dropdown value to reflect the current quality level
        _qualityDropdown.value = CurrentQualityIndex;

        // Set the quality level to the current setting when the game starts
        QualitySettings.SetQualityLevel(CurrentQualityIndex);
    }

    #endregion
}
