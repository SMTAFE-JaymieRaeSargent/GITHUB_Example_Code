using System.Collections.Generic;// Allows use of generic collections like List and Dictionary.
using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;// Enables interaction with Unity's built-in UI system, allowing for management of UI elements like Buttons, Text, and other components (non-TMPro).

/// <summary>
/// Handles changing the screen resolution using a dropdown.
/// Attach this script to a UI object and assign the Dropdown component.
/// </summary>
public class ResolutionManager : MonoBehaviour
{
    public Dropdown resolutionDropdown; // Reference to the UI Dropdown
    private Resolution[] availableResolutions; // All resolutions supported on this computer

    private void Start()
    {
        // Get all supported resolutions from the system
        availableResolutions = Screen.resolutions;

        // Clear any existing options in the dropdown
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        // Format each resolution and add it to the dropdown
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = $"{availableResolutions[i].width} x {availableResolutions[i].height}";
            options.Add(option);

            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Sets the screen resolution when an option is selected in the dropdown.
    /// </summary>
    /// <param name="selectedIndex">The index of the selected resolution.</param>
    public void SetResolution(int selectedIndex)
    {
        Resolution resolution = availableResolutions[selectedIndex];
        // Set resolution while keeping current screen mode
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }
}
