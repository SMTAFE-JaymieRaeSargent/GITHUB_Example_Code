using UnityEngine;// Required for MonoBehaviour and Unity-specific components in this case Screen
using System.IO;// Required for reading and writing files using File.ReadAllText and File.WriteAllText

/// <summary>
/// This class handles saving and loading player options such as resolution, audio, quality,
/// keybinds, and other preferences. It serializes the data to JSON and stores it in a file.
/// </summary>
public class SaveAndLoadOptions : MonoBehaviour
{
    #region File Path and Data

    // File path where the JSON options file will be saved.
    // Application.dataPath points to the project's Assets folder in the Editor
    // or to the root data folder in a build.
    string _filePath = $"{Application.dataPath}/OptionsData.json";

    // The main data object that holds all option settings.
    public OptionSaveData optionsData = new OptionSaveData();

    #endregion

    #region References to Other Option Scripts

    [Header("Options Scripts")] // Groups these fields in the Inspector for clarity

    [SerializeField] AudioManager _audioManager;                       // Manages volume levels
    [SerializeField] ResolutionManager _resolutionManager;            // Handles screen resolution changes
    [SerializeField] FullscreenModeManager _fullscreenModeManager;    // Handles fullscreen mode settings
    [SerializeField] QualityManager _qualityManager;                  // Manages graphics quality
    [SerializeField] CursorSelectManager _cursorSelectManager;        // Manages cursor icon selection
    [SerializeField] KeybindManager _keybindManager;                  // Handles keybind saving/loading

    #endregion

    #region Unity Methods

    /// <summary>
    /// Unity's Awake method is called before Start().
    /// Checks if a saved options file exists, and loads it if it does.
    /// </summary>
    private void Awake()
    {
        if (File.Exists(_filePath))
        {
            LoadOptions();
        }
    }

    #endregion

    #region Save Options

    /// <summary>
    /// Collects the current settings from the various manager scripts
    /// and stores them into the optionsData object.
    /// </summary>
    void GetDataToSave()
    {
        // Save the currently selected cursor index from the cursor manager.
        optionsData.currentCursor = _cursorSelectManager.CursorIndex;

        // Save whether mouse Y-axis inversion is currently enabled.
        optionsData.isMouseInverted = MouseInvertManager.IsInverted;

        // Save the names of actions for keybindings (e.g., "Forward", "Jumo").
        optionsData.keyNames = _keybindManager.SendKey();

        // Save the actual key values assigned to those actions (e.g., "W", "Space").
        optionsData.keyValues = _keybindManager.SendValue();

        // Save the current fullscreen mode (0 = Exclusive, 1 = Fullscreen Window, 2 = Windowed).
        optionsData.fullScreenMode = _fullscreenModeManager.CurrentFullscreenMode;

        // Save the current screen resolution width (e.g., 1920).
        optionsData.resolutionWidth = Screen.currentResolution.width;

        // Save the current screen resolution height (e.g., 1080).
        optionsData.resolutionHeight = Screen.currentResolution.height;

        // Save the index of the resolution dropdown (for UI matching later).
        optionsData.currentResolutionIndex = _resolutionManager.CurrentResolution;

        // Save the current graphics quality level (e.g., 0 = Low, 1 = Medium, 2 = High).
        optionsData.qualityLevel = _qualityManager.CurrentQualityIndex;

        // Save volume levels for different channels (e.g., Master, Music, SFX).
        optionsData.volume = _audioManager.VolumeControl;
    }

    /// <summary>
    /// Serializes the OptionSaveData object into a JSON string and saves it to a file.
    /// </summary>
    /// <param name="data">The data to save.</param>
    /// <param name="path">The file path where the data will be written.</param>
    void SaveJSON(OptionSaveData data, string path)
    {
        string lineToSave = JsonUtility.ToJson(data);       // Convert data to JSON string
        File.WriteAllText(path, lineToSave);                // Write string to file
    }

    /// <summary>
    /// Public method called to save all current settings to disk.
    /// Can be triggered by a UI button.
    /// </summary>
    public void SaveOptions()
    {
        GetDataToSave();                    // Gather current game settings
        SaveJSON(optionsData, _filePath);   // Save to file
    }

    #endregion

    #region Load Options

    /// <summary>
    /// Loads the JSON file from disk and deserializes it into an OptionSaveData object.
    /// </summary>
    /// <returns>The loaded OptionSaveData.</returns>
    OptionSaveData LoadData()
    {
        string loadedData = File.ReadAllText(_filePath);                     // Read JSON string
        return JsonUtility.FromJson<OptionSaveData>(loadedData);            // Convert JSON back to data object
    }

    /// <summary>
    /// Sends the loaded data back into the appropriate manager scripts.
    /// This restores the game settings visually and functionally.
    /// </summary>
    void SendDataFromLoad()
    {
        // Load the saved cursor index and apply it to the cursor manager.
        _cursorSelectManager.CursorIndex = optionsData.currentCursor;

        // Apply the saved mouse inversion setting (true = inverted, false = normal).
        MouseInvertManager.IsInverted = optionsData.isMouseInverted;

        // Restore all keybinds using saved names and their corresponding keys (e.g., "Jump" = "Space").
        _keybindManager.SetUpLoadedKeys(optionsData.keyNames, optionsData.keyValues);

        // Apply the selected resolution using the saved dropdown index.
        _resolutionManager.CurrentResolution = optionsData.currentResolutionIndex;

        // Set the screen mode (e.g., Fullscreen, Windowed, Borderless) from saved data.
        _fullscreenModeManager.CurrentFullscreenMode = optionsData.fullScreenMode;

        // Apply the saved graphics quality level (Low, Medium, High, etc.).
        _qualityManager.CurrentQualityIndex = optionsData.qualityLevel;

        // Restore the volume levels (Master, Music, SFX) from saved float array.
        _audioManager.VolumeControl = optionsData.volume;
    }

    /// <summary>
    /// Public method to load and apply saved options from disk.
    /// Can be triggered on game start or from an options menu.
    /// </summary>
    public void LoadOptions()
    {
        optionsData = LoadData();      // Load from disk
        SendDataFromLoad();            // Apply to game
    }

    #endregion   
}
