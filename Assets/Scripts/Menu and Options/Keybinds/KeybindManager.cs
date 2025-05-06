using System.Collections.Generic; // Allows use of generic collections like List and Dictionary.
using UnityEngine; // Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI; // Enables interaction with Unity's built-in UI system (non-TMPro).
using System; // Provides access to core system functions, including Enum parsing and serialization.

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
    // ==========================
    /*
     * [Serializable] allows Unity to display this struct in the Inspector
     * and enables data serialization (e.g., saving to files or sending over a network).
     */
    [Serializable]
    public struct ActionMapData
    {
        public string actionName;       // The name of the action (e.g., "Jump").
        public Text keycodeDisplay;     // The UI text that displays the assigned key.
        public string defaultKey;       // The default key as a string (e.g., "Space").
    }
    // ==========================
    #endregion

    #region Keybinding Variables
    // ==========================
    [SerializeField] ActionMapData[] _actionMapData; // Array of actions and their bindings shown in the Inspector.
    [SerializeField] GameObject _currentSelectedKey; // The UI button the player is currently modifying.
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); // Maps action names to KeyCodes.

    // Colors for visual feedback
    [SerializeField] private Color32 _selectedKey = new Color32(239, 116, 36, 225); // Color when selecting a new key.
    [SerializeField] private Color32 _changedKey = new Color32(39, 171, 249, 225);  // Color after key has been changed.
    // ==========================
    #endregion

    #region Initialization
    // ===================
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
            // Convert default key string to actual KeyCode and store it in the dictionary
            if (!keys.ContainsKey(_actionMapData[i].actionName))
            {
                keys.Add(_actionMapData[i].actionName, (KeyCode)Enum.Parse(typeof(KeyCode), _actionMapData[i].defaultKey));
            }

            // Update the UI to display the current key
            _actionMapData[i].keycodeDisplay.text = keys[_actionMapData[i].actionName].ToString();
        }

        /*
         * Summary:
         * - All default keybindings are loaded into the dictionary.
         * - The UI reflects those keybindings at startup.
         */
    }
    // ===================
    #endregion

    #region Change Keybinding
    // ===================
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
            _currentSelectedKey.GetComponent<Image>().color = _selectedKey;
        }
    }
    // ===================
    #endregion

    #region Listen for Input
    // ===================
    /// <summary>
    /// OnGUI is called multiple times per frame and used here to detect key presses.
    /// It listens for a key only if a keybinding change is in progress.
    /// </summary>
    private void OnGUI()
    {
        Event changeKeyEvent = Event.current; // Get the current UI/input event

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
    // ===================
    #endregion
}
