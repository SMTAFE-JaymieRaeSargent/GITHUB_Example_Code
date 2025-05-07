using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;// Enables interaction with Unity's built-in UI system (non-TMPro).
using UnityEngine.Audio;// Provides access to Unity's audio system, allowing manipulation of audio settings and audio sources in the game.

/// <summary>
/// This script controls audio mixer volume and updates a UI text display.
/// It uses exposed AudioMixer parameters (e.g., "Master", "Music", "SFX").
/// Attach it to a GameObject and assign the AudioMixer via the Inspector.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the AudioMixer in your project

    // Name of the currently targeted mixer channel (e.g., "Master", "Music", "SFX")
    private string _mixerChannel;

    // UI Text element that will show the volume percentage (e.g., "50%")
    private Text _channelVolumePercent;

    /// <summary>
    /// Called when selecting which mixer channel to adjust (via button or slider setup).
    /// Example: "Master", "Music", or "SFX" — must match the exposed parameter name in the mixer.
    /// </summary>
    public void CurrentMixer(string name)
    {
        _mixerChannel = name;
    }

    /// <summary>
    /// Called to assign the UI text that will display the volume percent.
    /// This should be done once before changing volume.
    /// </summary>
    public void GetText(Text uiText)
    {
        _channelVolumePercent = uiText;
    }

    /// <summary>
    /// Changes the volume of the currently selected mixer channel.
    /// Unity mixer values range from -80 (mute) to 20 (max volume).
    /// </summary>
    public void ChangeVolume(float volume)
    {
        // Set the volume for the selected mixer channel
        audioMixer.SetFloat(_mixerChannel, volume);

        // Update the UI text to show the new volume percent
        ChangeTextValue(volume);
    }

    /// <summary>
    /// Converts volume from mixer decibel value (-80 to 20) into a percent (0% to 100%),
    /// then updates the assigned UI text with that percentage.
    /// </summary>
    void ChangeTextValue(float volume)
    {
        // Convert decibel to a value between 0 and 1, then format as a percent string
        _channelVolumePercent.text = $"{Mathf.Clamp01((volume + 80) / 100):P0}";
    }
}