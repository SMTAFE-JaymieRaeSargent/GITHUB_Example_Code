using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.UI;
/// <summary>
/// This class manages the state of the mouse invert toggle.
/// It stores whether the mouse should be inverted based on the toggle's state.
/// The state can be set through the SetMouseInvertState function.
/// </summary>
public class MouseInvertManager : MonoBehaviour
{
    private static bool isInverted;// Private static variable to store the state of the mouse inversion (true = inverted, false = normal)
    [SerializeField] Toggle _invertedMouseToggle;// Serialized field to reference the UI toggle in the Inspector for mouse inversion

    /// <summary>
    /// Public property for accessing or setting the mouse inversion state.
    /// This acts as a getter and setter for the static 'isInverted' variable.
    /// </summary>
    public static bool IsInverted
    {
        // Sets the inversion state
        set 
        {
            // The values is assigned to the private _isInverted.
            isInverted = value; 
        }
        // Gets the current inversion state
        get 
        {
            //  Returns the private _isInverted value.
            return isInverted; 
        }
    }
    /// <summary>
    /// Sets the state of the mouse invert toggle.
    /// This function is called to change whether the mouse inversion is enabled or disabled.
    /// </summary>
    /// <param name="toggleValue">The state of the toggle, where true is inverted and false is not inverted.</param>
    public void SetMouseInvertState(bool toggleValue)
    {
        // Assign the passed toggle value (true or false) to the isInverted static variable
        IsInverted = toggleValue;
    }
    /// <summary>
    /// Unity's Start method is called when the script is first initialized.
    /// It ensures that the UI toggle reflects the current mouse invert state when the game starts.
    /// </summary>
    private void Start()
    {
        // Update the UI toggle to reflect the current mouse inversion state
        _invertedMouseToggle.isOn = isInverted;
    }
}
