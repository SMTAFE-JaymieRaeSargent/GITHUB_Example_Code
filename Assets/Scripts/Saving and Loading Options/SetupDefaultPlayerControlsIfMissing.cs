using System;// Needed for Enum.Parse (used to convert string to KeyCode)
using UnityEngine;// Uses UnityEngine for key input handling with KeyCode and MonoBehaviour to manage player control setup in the Unity scene.

/// <summary>
/// Initializes default player controls if no key bindings are currently set.
/// This ensures that the game has a functional control scheme on first launch or after a reset.
/// </summary>
public class SetupDefaultPlayerControlsIfMissing : MonoBehaviour
{
    /// <summary>
    /// Called when the script instance is being loaded.
    /// Checks if keybindings are empty, and sets up defaults if needed.
    /// </summary>
    void Awake()
    {
        // Check if no keybindings are currently stored in the KeybindManager.
        if (KeybindManager.keys.Count <= 0)
        {
            // Iterate through all default control names.
            for (int i = 0; i < DefaultControls.keyNames.Length; i++)
            {
                // Add each default keybinding to the key dictionary by adding the action name and Parse the string representation of the key into a KeyCode enum to the key bindings dictionary.
                KeybindManager.keys.Add(DefaultControls.keyNames[i], (KeyCode)Enum.Parse(typeof(KeyCode), DefaultControls.keyValues[i]));
            }
        }
    }
}
