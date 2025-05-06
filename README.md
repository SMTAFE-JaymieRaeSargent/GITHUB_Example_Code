# GITHUB_Example_Code
> The objective of this project is to design and develop a small but feature-complete Unity game prototype that simulates a simple RPG adventure experience. The game will integrate essential gameplay systems such as player movement, camera control, dialogue interactions, menus, and a basic saving/loading system. The project’s goal is to provide a cohesive gameplay experience where players can explore, interact, and progress through the adventure. The final product should highlight the development of a functional prototype that incorporates these systems seamlessly to create an engaging RPG experience. The project’s goal is to provide a cohesive gameplay experience where players can explore, interact, and progress through the adventure. The final product should highlight the development of a functional prototype that incorporates these systems seamlessly to create an engaging RPG experience.

[Basic writing and formatting syntax](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax)

## Team Members
1. James Sargent as Developer and Author
2. Jaymie-Rae Sargent as Reviewer

## Branching Naming Conventions
When working with GitHub, using consistent naming conventions for branches can improve collaboration and maintain clarity in your version control workflow. 
Here are some commonly followed naming conventions for branches:

### Feature Branch
Format: - `feature/{feature-name}`<br>
Example: - `feature/user-authentication`<br>
### Bugfix Branches
Format: - `bugfix/{bug-description}`<br>
Example: - `bugfix/fix-login-error`<br>
### Release Branches
Format: - `release/{version-number}`<br>
Example: - `release/v1.2.0`<br>

## Commit names
Every github commit should start with one of the following words:
- `modification`: when a new code is added or removed. Designate the file and the purpose of the modification. 
- `fix`: when a specific bug is fixed. 
- `file change`: if files are added or removed. 
- `refactor`: improved code without changing its behaviour
- `ui`: Add or modify UI Elements
## Features
[Creating task lists](https://docs.github.com/en/get-started/writing-on-github/working-with-advanced-formatting/about-task-lists#creating-task-lists)
### Menu and Options Menu
- [ ] The game will feature a main menu allowing players to start a new game, load a saved game, or exit.
- [ ]	The options menu will enable players to adjust sound settings, graphic preferences, and gameplay settings.
### Saving and Loading Options
- [ ]	The game will allow players to save their menu preferences, such as sound settings, graphic settings, and key bindings.
- [ ]	These settings will persist across sessions, ensuring that the player’s preferred configurations are automatically loaded when the game starts.
- [ ]	The system will store these preferences in a file format, enabling easy retrieval and modification.
### Player Movement
- [ ]	The player can move freely within the game world using keyboard inputs (WASD or arrow keys).
- [ ]	The Player can change speeds between crouch and sprint.
- [ ]	Movement is smooth and responsive, with a simple collision detection system to prevent the player from passing through objects.
### Camera Control
- [ ]	The camera will follow the player, providing a dynamic perspective of the game world.
- [ ]	The camera's position and rotation will adjust smoothly as the player moves, ensuring a clear and focused view of the action.
- [ ]	The player can optionally adjust the camera angle and zoom in/out using the mouse.
### Dialogue System
- [ ]	Players can read through text-based responses and make choices that affect the conversation.
- [ ]	Dialogue options will trigger different responses or actions based on player decisions.
### Interaction
- [ ]	The player can interact with NPCs, objects, and environments through simple prompts.
- [ ]	Interactions will trigger actions such as opening doors, picking up items, or starting dialogues.
- [ ]	The interaction system will be simple and context-sensitive, adapting to the object or NPC being interacted with.
### Stats and Leveling
- [ ]	The player will have stats such as health, experience points (XP), and level, which improve as they progress.
- [ ]	The leveling system will improve stats as the player advances.
### Saving and Loading Stats
- [ ]	Player stats will be saved during the game save process.
- [ ]	Stats will be stored in a file for easy management and retrieval when loading the game.
- [ ]	Any changes to the player’s stats (such as leveling up) will be automatically saved.
- [ ]	The player’s position, rotation, and other transform-related data will be saved when the game is saved.
- [ ]	When the game is loaded, the player will return to their exact last location and state, preserving the continuity of gameplay.
### Respawn
- [ ]	When the player’s character dies, they will respawn at a predefined location, such as a checkpoint or starting area.
- [ ]	Respawn will restore the player's health to a default value.
- [ ]	The respawn system will include a short delay before the player is returned to the game world to avoid instant re-engagement after death.
- [ ]	The game will provide a visual or audio cue to indicate the respawn event, ensuring the player is aware of their return to the game.
- [ ]	Player stats, including experience points and level, will remain intact upon respawn, maintaining the player’s progression despite death.

## Work Allocation
[Adding Tables](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/quickstart-for-writing-on-github#adding-a-table).

| Feature                    | Priority | Team Member   | Order | Order Reason                                                                                      |
|----------------------------|----------|---------------|-------|---------------------------------------------------------------------------------------------------|
| Main Menu and Options Menu | Urgent   | James Sargent | 1     | Develop UI for settings as keybinds will be needed for player                                     |
| Saving and Loading Options | Urgent   | James Sargent | 2     | Save and Load Keybinds and Mouse Invert option for the Player and Camera                          |
| Player Movement            | High     | James Sargent | 4     | Stats and Options will dictate functionality                                                      |
| Camera Control             | High     | James Sargent | 5     | Options will dictate functionality                                                                |
| Dialogue System            | Low      | James Sargent | 9     | To open the dialogue to test player and camera should allow movements, Interact is needed to open |
| Interface and Interaction  | Low      | James Sargent | 8     | Needs Keybinds for interaction key, Needs camera for aiming                                       |
| Stats and leveling         | Urgent   | James Sargent | 3     | Needed to be able to save stats, movement will pull stats for impact stamina                      |
| Saving and Loading Stats   | High     | James Sargent | 6     | Saving and loading will require stats and the players movements to exist for saving and loading   |
| Respawn                    | Low      | James Sargent | 7     | Respawn will require reloading stats and check point location from save file                      |
