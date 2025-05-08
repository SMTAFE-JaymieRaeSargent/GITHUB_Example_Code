using UnityEngine;// Grants access to Unity's core features, such as manipulating the screen's resolution and full-screen modes.
using UnityEngine.UI;// UnityEngine.UI namespace provides access to Unity's UI components like Toggle buttons.

/// <summary>
/// The FullscreenModeManager class handles the toggling of different screen modes (Exclusive Fullscreen, Fullscreen Windowed, and Windowed).
/// This script should be attached to a GameObject and linked to UI toggle elements to allow players to select their preferred display mode.
/// </summary>
public class FullscreenModeManager : MonoBehaviour
{
    #region Variables

    // Private variable to store the current fullscreen mode (0 = Exclusive Fullscreen, 1 = Fullscreen Windowed, 2 = Windowed)
    private int _fullscreenMode = 0;

    // UI toggle references for each screen mode option
    [Header("Toggle References")] // Custom header for organizing UI references in the Inspector
    [SerializeField] Toggle _exclusiveFullscreenToggle; // Toggle for Exclusive Fullscreen mode
    [SerializeField] Toggle _fullscreenWindowToggle; // Toggle for Fullscreen Windowed mode
    [SerializeField] Toggle _windowedToggle; // Toggle for Windowed mode

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the current fullscreen mode.
    /// This property stores the selected fullscreen mode (0, 1, or 2).
    /// </summary>
    public int CurrentFullscreenMode
    {
        // Set the current fullscreen mode value.
        set{ _fullscreenMode = value; }
        // Get the current fullscreen mode value.
        get { return _fullscreenMode; } 
    }

    #endregion

    #region Unity Callbacks

    /// <summary>
    /// Unity Start method is called before the first frame update.
    /// Initializes the UI toggle states based on the current fullscreen mode.
    /// </summary>
    private void Start()
    {
        // Check the current fullscreen mode and set the corresponding toggle to "on"
        switch (CurrentFullscreenMode)
        {
            case 0:
                // Exclusive Fullscreen is selected
                _exclusiveFullscreenToggle.isOn = true; 
                break;
            case 1:
                // Fullscreen Windowed is selected
                _fullscreenWindowToggle.isOn = true; 
                break;
            case 2:
                // Windowed mode is selected
                _windowedToggle.isOn = true; 
                break;
            default:
                // Default to Exclusive Fullscreen
                _exclusiveFullscreenToggle.isOn = true; 
                break;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the screen mode based on the selected mode (0, 1, or 2).
    /// This method is called when a player changes the screen mode through the UI.
    /// </summary>
    /// <param name="mode">The screen mode to set: 
    /// 0 for Exclusive Fullscreen, 
    /// 1 for Fullscreen Windowed, 
    /// 2 for Windowed.</param>
    public void SetScreenMode(int mode)
    {
        // Update the current fullscreen mode with the selected option
        CurrentFullscreenMode = mode;

        // Set the actual screen mode using Unity's Screen.fullScreenMode property
        switch (CurrentFullscreenMode)
        {
            case 0:
                // Exclusive Fullscreen mode
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; 
                break;
            case 1:
                // Fullscreen Windowed mode
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow; 
                break;
            case 2:
                // Windowed mode             
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                // Default to Exclusive Fullscreen
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; 
                break;
        }
    }

    #endregion
}