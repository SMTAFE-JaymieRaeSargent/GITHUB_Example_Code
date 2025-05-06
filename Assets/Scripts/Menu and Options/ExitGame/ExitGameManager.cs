using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.

/// <summary>
/// This class provides functionality to exit the game. It includes a method that handles quitting the game
/// both in the Unity Editor and in a standalone build.
/// </summary>
public class ExitGameManager : MonoBehaviour
{
    /// <summary>
    /// This method quits the game.
    /// - If running in the Unity Editor, it stops Play Mode.
    /// - If running as a built game, it closes the application.
    /// </summary>
    public void ExitToDesktop()
    {
        // This code only runs if the game is being played in the Unity Editor.
        // It stops the game from running in Play Mode.
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // This will quit the game when it is built and running as a standalone application.
        Application.Quit();
    }
}
