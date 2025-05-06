using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.

/// <summary>
/// Manages the game's quality settings.
/// Attach this script to a UI Dropdown or Button to allow players to select a quality level.
/// </summary>
public class QualityManager : MonoBehaviour
{
    /// <summary>
    /// Changes the game's quality level based on the index provided.
    /// Quality levels are defined in Edit > Project Settings > Quality.
    /// </summary>
    /// <param name="qualityIndex">The index of the quality level to set.</param>
    public void ChangeQuality(int qualityIndex)
    {
        // Sets the quality level using the given index (0 = lowest, higher numbers = better quality)
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
