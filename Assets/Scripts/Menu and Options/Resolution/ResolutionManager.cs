using System.Collections.Generic;// Allows use of generic collections like List and Dictionary.
using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;// Enables interaction with Unity's built-in UI system, allowing for management of UI elements like Buttons, Text, and other components (non-TMPro).

/// <summary>
/// Handles changing the screen resolution using a dropdown.
/// Attach this script to a UI object and assign the Dropdown component.
/// </summary>
public class ResolutionManager : MonoBehaviour
{
    [SerializeField] Dropdown _resolutionDropdown; // Reference to the UI Dropdown
    private Resolution[] _availableResolutions; // All resolutions supported on this computer
    int _currentResolutionIndex = -1;

    public int CurrentResolution
    {
        set { _currentResolutionIndex = value; }
        get { return _currentResolutionIndex; }
    }
    private void Start()
    {
        // Get all supported resolutions from the system
        _availableResolutions = Screen.resolutions;

        // Clear any existing options in the dropdown
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        // Format each resolution and add it to the dropdown
        for (int i = 0; i < _availableResolutions.Length; i++)
        {
            string option = $"{_availableResolutions[i].width} x {_availableResolutions[i].height}";
            if (!options.Contains(option))
            {
                options.Add(option);
            }
            if (CurrentResolution == -1)
            {
                if (_availableResolutions[i].width == Screen.currentResolution.width &&
                _availableResolutions[i].height == Screen.currentResolution.height)
                {
                    CurrentResolution = i;

                }
            }            
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = CurrentResolution;
        _resolutionDropdown.RefreshShownValue();
    }
   
    /// <summary>
    /// Sets the screen resolution when an option is selected in the dropdown.
    /// </summary>
    /// <param name="selectedIndex">The index of the selected resolution.</param>
    public void SetResolution(int selectedIndex)
    {
        CurrentResolution = selectedIndex;
        Resolution resolution = _availableResolutions[CurrentResolution];
        // Set resolution while keeping current screen mode
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }
}
