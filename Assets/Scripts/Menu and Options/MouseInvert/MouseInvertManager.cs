using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;
/// <summary>
/// This class manages the state of the mouse invert toggle.
/// It stores whether the mouse should be inverted based on the toggle's state.
/// The state can be set through the SetMouseInvertState function.
/// </summary>
public class MouseInvertManager : MonoBehaviour
{
    // Reference to store the state of the mouse invert toggle
    private static bool isInverted;
    [SerializeField] Toggle _invertedMouseToggle;

    public static bool IsInverted
    {
        set { isInverted = value; }
        get { return isInverted; }
    }
    /// <summary>
    /// Sets the state of the mouse invert toggle.
    /// This function is called to change whether the mouse inversion is enabled or disabled.
    /// </summary>
    /// <param name="toggleValue">The state of the toggle, where true is inverted and false is not inverted.</param>
    public void SetMouseInvertState(bool toggleValue)
    {
        // Assign the passed toggle value to the isInverted variable
        IsInverted = toggleValue;
    }
    private void Start()
    {
        _invertedMouseToggle.isOn = isInverted;
    }
}
