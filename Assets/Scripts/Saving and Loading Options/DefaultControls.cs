/// <summary>
/// Stores the default key bindings for the game.
/// These are used to initialize controls or reset them to default settings.
/// </summary>
public static class DefaultControls
{
    // Names of the actions that can be controlled by the player.
    // These should match what's displayed in the game's keybinding UI.
    public static string[] keyNames = new string[]
    {
        "Forward",   // Move forward
        "Backward",  // Move backward
        "Left",      // Move left
        "Right",     // Move right
        "Jump",      // Jump action
        "Sprint",    // Sprint or run
        "Crouch",    // Crouch or sneak
        "Interact"   // Interact with objects or NPCs
    };

    // Default keys assigned to each action above.
    // These are the actual keyboard keys that will trigger the actions.
    public static string[] keyValues = new string[]
    {
        "W",             // Forward
        "S",             // Backward
        "A",             // Left
        "D",             // Right
        "Space",         // Jump
        "LeftShift",     // Sprint
        "LeftControl",   // Crouch
        "E"              // Interact
    };
}
