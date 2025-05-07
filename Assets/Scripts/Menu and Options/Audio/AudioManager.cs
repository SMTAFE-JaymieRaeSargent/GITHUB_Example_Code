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

    [SerializeField] Slider[] _slider = new Slider[3];
    [SerializeField] Text[] _percentText = new Text[3];
    [SerializeField] float[] _volume = new float[3];
    [SerializeField] string[] _channelName = new string[3];

    public float[] VolumeControl
    {
        set 
        {
            _volume = value; 
        }
        get { return _volume; }
    }

    private void Start()
    {
        for (int i = 0; i < _volume.Length; i++)
        {
            _slider[i].value = VolumeControl[i];
            audioMixer.SetFloat(_channelName[i], VolumeControl[i]);
            _percentText[i].text = $"{Mathf.Clamp01((VolumeControl[i] + 80) / 100):P0}";

        }

    }
    
    public void ChangeVolume(int volumeID)
    {
            VolumeControl[volumeID] = _slider[volumeID].value;
            audioMixer.SetFloat(_channelName[volumeID], VolumeControl[volumeID]);
            _percentText[volumeID].text = $"{Mathf.Clamp01((VolumeControl[volumeID] + 80) / 100):P0}";       
    }
}