using UnityEngine; // Grants access to Unity-specific features like MonoBehaviour, GameObjects, AudioSource, etc.

/// <summary>
/// Manages persistent audio in the game, including background music and sound effects.
/// This script uses a Singleton pattern to ensure only one instance exists and persists between scenes.
/// </summary>
public class PersistantAudioAsset : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// The globally accessible instance of the audio manager.
    /// </summary>
    public static PersistantAudioAsset instance;

    /// <summary>
    /// The prefab to instantiate if the audio object does not already exist.
    /// </summary>
    public GameObject objectToSpawn;

    /// <summary>
    /// The actual spawned instance of the audio prefab.
    /// </summary>
    private static GameObject spawnedObject;

    /// <summary>
    /// The AudioSource used for playing music clips.
    /// </summary>
    private static AudioSource musicSource;

    /// <summary>
    /// The AudioSource used for playing sound effects (SFX).
    /// </summary>
    private static AudioSource sfxSource;

    #endregion

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Ensures only one instance of the audio manager exists and persists across scenes.
    /// </summary>
    private void Awake()
    {
        // If no instance exists yet, set this as the instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive between scene loads

            // Spawn the audio prefab if it hasn't already been created
            if (objectToSpawn != null && spawnedObject == null)
            {
                spawnedObject = Instantiate(objectToSpawn);
                DontDestroyOnLoad(spawnedObject); // Keep the spawned object alive too

                // Attempt to retrieve multiple AudioSources on the spawned object
                AudioSource[] sources = spawnedObject.GetComponents<AudioSource>();

                // If at least two AudioSources exist, assign them to music and SFX
                if (sources.Length >= 2)
                {
                    musicSource = sources[0]; // Typically used for background music
                    sfxSource = sources[1];   // Typically used for sound effects
                }
            }
        }
        // If another instance already exists that's not this one, destroy this duplicate
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays a one-shot sound effect clip using the SFX audio source.
    /// </summary>
    /// <param name="clip">The AudioClip to play as a sound effect.</param>
    public void PlaySFX(AudioClip clip)
    {
        // Only play if the SFX source and clip are valid
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays a looping music track using the music audio source.
    /// Replaces any currently playing music.
    /// </summary>
    /// <param name="clip">The AudioClip to play as background music.</param>
    public void PlayMusic(AudioClip clip)
    {
        // Only play if the music source and clip are valid
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
