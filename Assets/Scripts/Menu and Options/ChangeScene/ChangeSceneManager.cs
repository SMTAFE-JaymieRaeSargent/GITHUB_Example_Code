using UnityEngine;// Grants access to core Unity functionality, including MonoBehaviour, GameObjects, and other Unity-related classes and methods.
using UnityEngine.SceneManagement;// Required to access scene management functions, such as loading, unloading, and transitioning between scenes.

/// <summary>
/// This class provides methods for managing scene transitions in Unity. It allows loading a new scene
/// based on either the scene's name, build index, or by reloading the current scene. Additionally, it includes
/// functionality for loading the next scene in the build order.
/// </summary>
public class SceneChangeManager : MonoBehaviour
{
    /// <summary>
    /// Loads a new scene by using the scene's name.
    /// Make sure the scene name is spelled correctly and is added to the Build Settings.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads a new scene by using the scene's build index.
    /// The index is set in File > Build Settings.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load.</param>
    public void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Reloads the currently active scene.
    /// Useful for restarting a level.
    /// </summary>
    public void ReloadCurrentScene()
    {
        // Get the current scene's build index and load it again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads the next scene based on the current scene's index.
    /// Make sure the next scene is added in the Build Settings.
    /// </summary>
    public void LoadNextScene()
    {
        // Increase the current build index by 1 to load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
