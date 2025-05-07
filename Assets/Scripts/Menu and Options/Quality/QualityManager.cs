using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;
/// <summary>
/// Manages the game's quality settings.
/// Attach this script to a UI Dropdown or Button to allow players to select a quality level.
/// </summary>
public class QualityManager : MonoBehaviour
{
    [SerializeField] Dropdown _qualityDropdown; // Reference to the UI Dropdown
    int _currentqualityIndex = 0;

    public int CurrentQualityIndex
    {
        set { _currentqualityIndex = value; }
        get { return _currentqualityIndex; }
    }
    /// <summary>
    /// Changes the game's quality level based on the index provided.
    /// Quality levels are defined in Edit > Project Settings > Quality.
    /// </summary>
    /// <param name="qualityIndex">The index of the quality level to set.</param>
    public void ChangeQuality(int qualityIndex)
    {
        CurrentQualityIndex = qualityIndex;
        // Sets the quality level using the given index (0 = lowest, higher numbers = better quality)
        QualitySettings.SetQualityLevel(CurrentQualityIndex);
    }
    private void Start()
    {
        _qualityDropdown.value = CurrentQualityIndex;
        QualitySettings.SetQualityLevel(CurrentQualityIndex);
    }
}
