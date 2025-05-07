using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;

public class FullscreenModeManager : MonoBehaviour
{
    private int _fullscreenMode = 0;
    [Header("Toggle References")]
    [SerializeField] Toggle _exclusiveFullscreenToggle;
    [SerializeField] Toggle _fullscreenWindowToggle;
    [SerializeField] Toggle _windowedToggle;

    public int CurrentFullscreenMode
    {
        set
        {
            _fullscreenMode = value;
        }
        get { return _fullscreenMode; }
    }
    private void Start()
    {
        switch (CurrentFullscreenMode)
        {
            case 0:
                _exclusiveFullscreenToggle.isOn = true;

                break;
            case 1:
                _fullscreenWindowToggle.isOn = true;

                break;
            case 2:
                _windowedToggle.isOn = true;

                break;
            default:
                _exclusiveFullscreenToggle.isOn = true;
                break;
        }
    }
    public void SetScreenMode(int mode)
    {
        CurrentFullscreenMode = mode;
        switch (CurrentFullscreenMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;

                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;

                break;
            default:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

                break;
        }
    }
}
