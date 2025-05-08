using System.Collections.Generic;// Allows use of generic collections like List and Dictionary.
using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;// Enables interaction with Unity's built-in UI system, allowing for management of UI elements like Buttons, Text, and other components (non-TMPro).

/// <summary>
/// Handles changing the screen resolution using a dropdown.
/// It allows users to select from a list of available screen resolutions supported by their system.
/// Attach this script to a GameOnject object and assign the Dropdown component.
/// </summary>
public class ResolutionManager : MonoBehaviour
{
    #region Variables

    // Reference to the UI Dropdown component used for selecting screen resolutions.
    [SerializeField] Dropdown _resolutionDropdown;

    // Array to store all the available screen resolutions supported on the device.
    private Resolution[] _availableResolutions;

    // Integer to track the current selected resolution index.
    int _currentResolutionIndex = -1;

    #endregion

    #region Properties

    /// <summary>
    /// Property to get or set the current selected resolution index.
    /// </summary>
    public int CurrentResolution
    {
        // Set the resolution index.
        set { _currentResolutionIndex = value; } 
        // Get the resolution index.
        get { return _currentResolutionIndex; } 
    }

    #endregion

    #region Unity Callbacks

    /// <summary>
    /// Unity's Start method, called before the first frame update.
    /// Initializes the dropdown options with available resolutions and selects the current resolution.
    /// </summary>
    private void Start()
    {
        // Get all supported resolutions from the system (Screen.resolutions provides all available screen resolutions).
        _availableResolutions = Screen.resolutions;

        // Clear any existing options in the dropdown to avoid duplicates.
        _resolutionDropdown.ClearOptions();

        // List to hold the formatted string representations of the available resolutions.
        List<string> options = new List<string>();

        // Loop through the available resolutions and format them into a list for the dropdown.
        for (int i = 0; i < _availableResolutions.Length; i++)
        {
            // Create a string in the format "width x height" (e.g., "1920 x 1080").
            string option = $"{_availableResolutions[i].width} x {_availableResolutions[i].height}";

            // Ensure that the resolution is not added multiple times to the dropdown (avoid duplicates).
            if (!options.Contains(option))
            {
                options.Add(option);
            }

            // Automatically select the current screen resolution if it's not already set.
            if (CurrentResolution == -1)
            {
                // Check if the current resolution matches one of the available resolutions.
                if (_availableResolutions[i].width == Screen.currentResolution.width &&
                    _availableResolutions[i].height == Screen.currentResolution.height)
                {
                    // Set the selected resolution index to the matching resolution.
                    CurrentResolution = i;
                }
            }
        }

        // Add all the formatted resolution options to the dropdown.
        _resolutionDropdown.AddOptions(options);

        // Set the dropdown value to the currently selected resolution.
        _resolutionDropdown.value = CurrentResolution;

        // Refresh the dropdown to update the displayed value.
        _resolutionDropdown.RefreshShownValue();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the screen resolution when an option is selected in the dropdown.
    /// This method is triggered by the dropdown's OnValueChanged event.
    /// </summary>
    /// <param name="selectedIndex">The index of the selected resolution in the dropdown.</param>
    public void SetResolution(int selectedIndex)
    {
        // Update the current resolution index to the selected option.
        CurrentResolution = selectedIndex;

        // Get the resolution object for the selected index.
        Resolution resolution = _availableResolutions[CurrentResolution];

        // Set the screen resolution to the selected resolution while maintaining the current full-screen mode.
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    #endregion
}