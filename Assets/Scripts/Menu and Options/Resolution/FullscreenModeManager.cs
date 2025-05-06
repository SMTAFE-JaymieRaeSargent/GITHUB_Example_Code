using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.

/// <summary>
/// Handles changing the screen mode (Fullscreen, Windowed, Borderless).
/// Attach each method to a UI toggle (only one should be active at a time).
/// </summary>
public class FullscreenModeManager : MonoBehaviour
{
    /// <summary>
    /// Sets the display to exclusive fullscreen mode.
    /// </summary>
    public void SetFullscreenMode()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    /// <summary>
    /// Sets the display to windowed mode (resizable window).
    /// </summary>
    public void SetWindowedMode()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    /// <summary>
    /// Sets the display to fullscreen window (borderless fullscreen).
    /// </summary>
    public void SetBorderlessMode()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
}
