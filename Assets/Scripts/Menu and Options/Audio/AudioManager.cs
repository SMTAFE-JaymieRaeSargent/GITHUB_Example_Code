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
    [Header("Audio Mixer")]
    public AudioMixer audioMixer; // Reference to the AudioMixer in your project  
    [Header("Volume Control Sliders")]
    [SerializeField] Slider[] _slider = new Slider[3]; // Sliders to control volume for different channels
    [SerializeField] Text[] _percentText = new Text[3]; // Text elements to display volume percentage for each channel
    [SerializeField] float[] _volume = new float[3]; // The actual volume values (ranging from -80 to 0)
    [SerializeField] string[] _channelName = new string[3]; // The names of the audio mixer channels (e.g., "Master", "Music", "SFX")
    /// <summary>
    /// Gets or sets the volume levels for the audio mixer channels.
    /// The values are stored as an array, with each index corresponding to a different channel (e.g., Master, Music, SFX).
    /// The values range from -80 (silence) to 20 (full volume).
    /// </summary>
    public float[] VolumeControl
    {
        // Sets the volume levels for all audio mixer channels
        set
        {
            // The provided array of values is assigned to the private _volume array
            _volume = value; 
        }
        // Gets the current volume levels for all audio mixer channels
        get 
        {
            // Returns the _volume array containing volume values for each channel
            return _volume;
        }
    }
    /// <summary>
    /// Initializes the volume levels for each audio channel and updates the UI elements accordingly.
    /// It sets the slider values based on the stored volume levels, updates the AudioMixer parameters,
    /// and displays the corresponding volume percentage on the UI text elements.
    /// </summary>
    private void Start()
    {
        // Initialize the volume levels for each channel and update the UI
        for (int i = 0; i < _volume.Length; i++)
        {
            // Set the slider value to the stored volume level
            _slider[i].value = VolumeControl[i];
            // Set the AudioMixer parameter based on the volume
            audioMixer.SetFloat(_channelName[i], VolumeControl[i]);
            // Update the UI text to show the volume percentage (from 0% to 100%)
            _percentText[i].text = $"{Mathf.Clamp01((VolumeControl[i] + 80) / 100):P0}";
        }
    }
    /// <summary>
    /// Called when the user changes the volume using the slider.
    /// Updates the AudioMixer and the UI text to reflect the new volume.
    /// </summary>
    /// <param name="volumeID">The index of the channel being modified (e.g., Master, Music, or SFX)</param>
    public void ChangeVolume(int volumeID)
    {
        // Update the volume value from the slider
        VolumeControl[volumeID] = _slider[volumeID].value;
        // Set the corresponding AudioMixer parameter to the new volume
        audioMixer.SetFloat(_channelName[volumeID], VolumeControl[volumeID]);
        // Update the UI text to show the new volume percentage
        _percentText[volumeID].text = $"{Mathf.Clamp01((VolumeControl[volumeID] + 80) / 100):P0}";       
    }
}