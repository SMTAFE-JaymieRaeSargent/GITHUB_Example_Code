using UnityEngine;
using System;

[Serializable]
public class OptionSaveData
{
    [Header("Resolution Fullscreen")]
    public int fullScreenMode;
    public int resolutionWidth;
    public int resolutionHeight;
    public int currentResolutionIndex;
    [Header("Audio")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    [Header("Quality")]
    public int qualityLevel;
    [Header("Controls")]
    public string[] keyNames;
    public string[] keyValues;
    public bool isMouseInverted;
    public int currentCursor;
}
