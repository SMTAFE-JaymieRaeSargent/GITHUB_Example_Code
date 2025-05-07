using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.

/// <summary>
/// The CursorSelectManager class allows dynamic cursor changes when interacting with UI buttons.
/// Each button can be assigned a unique cursor texture, which will change the cursor whenever the button is clicked.
/// This script should be attached to a GameObject and the `ChangeCursor` method should be assigned to the OnClick event of the buttons.
/// </summary>
public class CursorSelectManager : MonoBehaviour
{
    [SerializeField] Texture2D[] _cursors;
    private int _currentCursorIndex;

    public int CursorIndex
    {
        set 
        { 
            _currentCursorIndex = value;
            Cursor.SetCursor(_cursors[_currentCursorIndex], Vector2.zero, CursorMode.Auto);

        }
        get { return _currentCursorIndex; }
    }
    /// <summary>
    /// Changes the cursor to the corresponding one when a button is clicked.
    /// This function is to be assigned to each button's OnClick event.
    /// </summary>
    public void ChangeCursor(int i)
    {
        // Set the cursor to the selected one
        CursorIndex = i;
    }
}