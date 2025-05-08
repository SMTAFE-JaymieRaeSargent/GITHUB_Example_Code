using UnityEngine;
using System.IO;
using System;




public class SaveAndLoadOptions : MonoBehaviour
{
    string _filePath = $"{Application.dataPath}/OptionsData.json";
    public OptionSaveData optionsData = new OptionSaveData();
    [Header("Options Scripts")]
    [SerializeField] AudioManager _audioManager;
    [SerializeField] ResolutionManager _resolutionManager;
    [SerializeField] FullscreenModeManager _fullscreenModeManager;
    [SerializeField] QualityManager _qualityManager;
    [SerializeField] CursorSelectManager _cursorSelectManager;
    [SerializeField] KeybindManager _keybindManager;


    private void Awake()
    {      
        if (File.Exists(_filePath))
        {
            LoadOptions();
        }
    }
  
    #region Save Options  
    /*
    Function to get data ready to send
    Function to write data to file
    Function to run both functions
     */
    void GetDataToSave()
    {
       optionsData.currentCursor = _cursorSelectManager.CursorIndex;
       optionsData.isMouseInverted = MouseInvertManager.IsInverted;
       optionsData.keyNames = _keybindManager.SendKey();
       optionsData.keyValues = _keybindManager.SendValue();

        optionsData.fullScreenMode = _fullscreenModeManager.CurrentFullscreenMode;
        optionsData.resolutionWidth = Screen.currentResolution.width;
        optionsData.resolutionHeight = Screen.currentResolution.height;
        optionsData.currentResolutionIndex = _resolutionManager.CurrentResolution; 

        optionsData.qualityLevel = _qualityManager.CurrentQualityIndex;

        optionsData.volume = _audioManager.VolumeControl;

    }   
    void SaveJSON(OptionSaveData data, string path)
    {
        string lineToSave = JsonUtility.ToJson(data);
        File.WriteAllText(path, lineToSave);
    }
    public void SaveOptions()
    {
        GetDataToSave();
        SaveJSON(optionsData,_filePath);
    }
    #endregion
    #region Load
    /*
    Function to read data from file
    Function to send data to other scripts
    Function to run both functions
     */
    OptionSaveData LoadData()
    {
        string loadedData = File.ReadAllText(_filePath);
        return JsonUtility.FromJson<OptionSaveData>(loadedData);
    }
    void SendDataFromLoad()
    {
        _cursorSelectManager.CursorIndex = optionsData.currentCursor;
        MouseInvertManager.IsInverted = optionsData.isMouseInverted;
        _keybindManager.SetUpLoadedKeys(optionsData.keyNames, optionsData.keyValues);
        _resolutionManager.CurrentResolution = optionsData.currentResolutionIndex;
        _fullscreenModeManager.CurrentFullscreenMode = optionsData.fullScreenMode;
        _qualityManager.CurrentQualityIndex = optionsData.qualityLevel;
        _audioManager.VolumeControl = optionsData.volume;
    }
    public void LoadOptions()
    {
        optionsData = LoadData();
        SendDataFromLoad();
    }
    #endregion   
}
