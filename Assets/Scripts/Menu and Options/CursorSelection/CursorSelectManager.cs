using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.

/// <summary>
/// The CursorSelectManager class allows dynamic cursor changes when interacting with UI buttons.
/// Each button can be assigned a unique cursor texture, which will change the cursor whenever the button is clicked.
/// This script should be attached to a GameObject and the `ChangeCursor` method should be assigned to the OnClick event of the buttons.
/// </summary>
public class CursorSelectManager : MonoBehaviour
{
    #region Variables and UI References

    // Array to store different cursor textures (each element corresponds to a unique cursor)
    [SerializeField] Texture2D[] _cursors;

    // Private variable to track the index of the current cursor
    private int _currentCursorIndex;

    #endregion

    #region Properties

    /// <summary>
    /// Public property to get or set the current cursor index.
    /// When setting, it updates the cursor to the new texture at the given index.
    /// </summary>
    public int CursorIndex
    {
        set
        {
            // Update the cursor index and set the corresponding cursor texture
            _currentCursorIndex = value;
            // Set the cursor to the new texture at the specified index
            Cursor.SetCursor(_cursors[_currentCursorIndex], Vector2.zero, CursorMode.Auto);
        }
        get { return _currentCursorIndex; } // Return the current cursor index
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Changes the cursor to the corresponding one when a button is clicked.
    /// This function should be assigned to the OnClick event of buttons.
    /// </summary>
    /// <param name="i">The index of the cursor texture to switch to.</param>
    public void ChangeCursor(int i)
    {
        // Set the cursor to the texture at the given index
        CursorIndex = i;
    }

    #endregion
}