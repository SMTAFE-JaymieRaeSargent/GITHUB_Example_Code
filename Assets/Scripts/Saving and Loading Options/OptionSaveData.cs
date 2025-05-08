using UnityEngine;// Required for using Unity-specific types and functions.
using System;// Required to make custom data types savable and visible in the Inspector with [Serializable].


/// <summary>
/// Serializable data container for saving player options such as
/// resolution, audio, quality, and control settings.
/// </summary>
[Serializable]
public class OptionSaveData
{
    #region Resolution and Fullscreen

    [Header("Resolution Fullscreen")]

    /// <summary>
    /// The full screen mode selected by the player.
    /// 0 = Exclusive Fullscreen, 1 = Fullscreen Window, 2 = Windowed
    /// </summary>
    public int fullScreenMode;

    /// <summary>
    /// The width of the selected resolution.
    /// </summary>
    public int resolutionWidth;

    /// <summary>
    /// The height of the selected resolution.
    /// </summary>
    public int resolutionHeight;

    /// <summary>
    /// The index of the selected resolution in the resolution dropdown.
    /// Used to match the saved setting with the options shown in the UI.
    /// </summary>
    public int currentResolutionIndex;

    #endregion

    #region Audio

    [Header("Audio")]

    /// <summary>
    /// Array of volume levels.
    /// Each index corresponds to a specific channel (e.g., 0 = Master, 1 = Music, 2 = SFX).
    /// </summary>
    public float[] volume;

    #endregion

    #region Quality Settings

    [Header("Quality")]

    /// <summary>
    /// Index of the selected quality level (matches Unity's quality settings).
    /// 0 = Low, 1 = Medium, 2 = High
    /// </summary>
    public int qualityLevel;

    #endregion

    #region Controls

    [Header("Controls")]

    /// <summary>
    /// Names of the control keys (e.g."Forward").
    /// Used to display key bindings in the UI.
    /// </summary>
    public string[] keyNames;

    /// <summary>
    /// The actual key values assigned to the actions (e.g., "W").
    /// </summary>
    public string[] keyValues;

    /// <summary>
    /// Whether the mouse Y-axis is inverted.
    /// </summary>
    public bool isMouseInverted;

    /// <summary>
    /// Index of the currently selected cursor icon.
    /// </summary>
    public int currentCursor;

    #endregion
}
