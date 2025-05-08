using System.Collections.Generic; // Allows use of generic collections like List and Dictionary.
using UnityEngine; // Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI; // Enables interaction with Unity's built-in UI system (non-TMPro).
using System;

/// <summary>
/// Handles in-game keybinding functionality, including:
/// - Displaying default keybinds in the UI,
/// - Allowing players to rebind keys at runtime,
/// - Updating both the UI and internal mappings when changes occur.
/// Attach this script to a manager GameObject and configure via the Inspector.
/// </summary>
public class KeybindManager : MonoBehaviour
{
    #region Data Structure Definition
    /// <summary>
    /// Holds information for each action's keybinding.
    /// </summary>
    [Serializable]
    public struct ActionMapData
    {
        public string actionName;       // The name of the action (e.g., "Jump").
        public Text keycodeDisplay;     // The UI text that displays the assigned key.
        public string defaultKey;       // The default key as a string (e.g., "Space").
    }
    #endregion

    #region Keybinding Variables
    [Header("Action Mapping")] 
    [SerializeField] ActionMapData[] _actionMapData; // Array of actions and their bindings shown in the Inspector.
    [Header("UI Feedback")]
    [SerializeField] GameObject _currentSelectedKey; // The UI button the player is currently modifying.
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); // Maps action names to KeyCodes.

    // Colors for visual feedback
    [SerializeField] private Color32 _selectedKey = new Color32(239, 116, 36, 225); // Color when selecting a new key.
    [SerializeField] private Color32 _changedKey = new Color32(39, 171, 249, 225);  // Color after key has been changed.
    #endregion
    /// <summary>
    /// Sets up the keys from a loaded configuration.
    /// </summary>
    public void SetUpLoadedKeys(string[] key, string[] value)
    {
        // Clear the existing key mappings.
        keys.Clear();
        // Iterate through the key and value arrays to populate the dictionary
        for (int i = 0; i < key.Length; i++)  
        {
            // Add the key-value pair to the dictionary. Parse the value into KeyCode.
            keys.Add(key[i], (KeyCode)Enum.Parse(typeof(KeyCode), value[i]));
        }
    }
    /// <summary>
    /// Returns the key names as an array as Json Utitity doesnt handle Dictionaries.
    /// </summary>
    public string[] SendKey()
    {
        // Create an array to hold the key names.
        string[] tempKey = new string[keys.Count];
         // Create an int to hold the current array element.
        int i = 0;
        // Loop through the dictionary to extract key names.
        foreach (KeyValuePair<string, KeyCode> key in keys)
        {
            // Store the key name in the array.
            tempKey[i] = key.Key;
            // Move to the next index.
            i++;
        }
        // Return the key names.
        return tempKey;
    }
    /// <summary>
    /// Returns the key values as an array.
    /// </summary>
    public string[] SendValue()
    {
        // Create an array to hold the key values.
        string[] tempValue = new string[keys.Count];
        // Create an int to hold the current array element.
        int i = 0;
        // Loop through the dictionary to extract key values.
        foreach (KeyValuePair<string, KeyCode> key in keys)
        {
            // Store the key value (KeyCode) as a string in the array.
            tempValue[i] = key.Value.ToString();
            // Move to the next index.
            i++;
        }
        // Return the key values.
        return tempValue;
    }

    #region Initialization
    /// <summary>
    /// Initializes the keybindings and updates the UI with the current bindings.
    /// This method populates the `keys` dictionary with default key assignments
    /// and sets the corresponding UI text to display the current keys.
    /// </summary>
    private void Start()
    {
        // Loop through each action in the keybinding array
        for (int i = 0; i < _actionMapData.Length; i++)
        {
            // Check if the action is already in the dictionary
            if (!keys.ContainsKey(_actionMapData[i].actionName))
            {
                // Add the action with its default key if not already in the dictionary
                keys.Add(_actionMapData[i].actionName, (KeyCode)Enum.Parse(typeof(KeyCode), _actionMapData[i].defaultKey));
            }

            // Update the UI to display the current key (show the assigned key in the text component)
            _actionMapData[i].keycodeDisplay.text = keys[_actionMapData[i].actionName].ToString();
        }
    }
    #endregion

    #region Change Keybinding
    /// <summary>
    /// Called when the user clicks a UI button to change a keybinding.
    /// The button's name must match the action name in the dictionary.
    /// </summary>
    public void ChangeKey(GameObject clickedKey)
    {
        // Store the clicked button as the current one being edited
        _currentSelectedKey = clickedKey;

        // Visually indicate it's ready for key input
        if (_currentSelectedKey != null)
        {
            // Change the button color to indicate selection
            _currentSelectedKey.GetComponent<Image>().color = _selectedKey;
        }
    }
    #endregion

    #region Listen for Input
    /// <summary>
    /// OnGUI is called multiple times per frame and used here to detect key presses.
    /// It listens for a key only if a keybinding change is in progress.
    /// </summary>
    private void OnGUI()
    {
        // Get the current UI/input event
        Event changeKeyEvent = Event.current; 

        // Check if there's a button selected and the event is a key press
        if (_currentSelectedKey != null && changeKeyEvent.isKey) 
        {
            // Only continue if the key isn't already assigned
            if (!keys.ContainsValue(changeKeyEvent.keyCode))
            {
                // Update the dictionary with the new key
                keys[_currentSelectedKey.name] = changeKeyEvent.keyCode;

                // Update the button text to reflect the new key
                _currentSelectedKey.GetComponentInChildren<Text>().text = changeKeyEvent.keyCode.ToString();

                // Change button color to indicate success
                _currentSelectedKey.GetComponent<Image>().color = _changedKey;

                // Clear the selection (reset for next use)
                _currentSelectedKey = null;
            }
        }
    }
    #endregion
}
