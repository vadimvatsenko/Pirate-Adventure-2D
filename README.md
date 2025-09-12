
![c#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Unity](https://img.shields.io/badge/Unity-2022.3+-lightgrey)
![Platformer2D](https://img.shields.io/badge/genre-platformer2D-blue)

## A small 2D platformer prototype in Unity about a cheeky pirate, treasure, and trouble at sea.

### ✨ Features

- ⚓️ Tight 2D platformer controls (run, jump, coyote time, variable jump)
- 🗡️ Melee & ranged attack foundation - in progress
- 🧠 State‑based player/enemy logic (FSM)
- 🌴 Parallax backgrounds, tileset‑based levels
- 🎵 Basic SFX/music hooks - in future
- 🧰 Simple save/checkpoint system - in future

### 📦 Tech Stack
- Unity (LTS recommended, e.g. 2022.3.x)
- C# scripts
- Unity 2D
- Cinemachine (camera)
- New Input System

### 🎮 Controls
- Move - A/D
- Jump - Space
- Attack - Mouse 0
- Interact - R

### 🗂️ Project Structure 

```sh
Pirate-Adventures-2D/
│── PlayerInput/      # New Input System     
│── Resources/        # Game Resources
   │── Animations     # Animation Files
   │── Camera         # Camera Blends
   │── Fonts          # InGame Fonts
   │── Gradient       # Color Gradients
   │── Materials      # Materials
   │── Prefabs        # Game Prefab
   │── RenderPipeLine # Lights
   │── Sprites        # Sprites
   │── TilePalette    # Tiles
│── Scenes/           # Game Scenes
│── Scripts/          # Scripts
│── README.md         # This File
```

### 🧱 Code Highlights / Scripts
`Cam`
- `CameraBoundsSwitcher.cs` - camera switcher
- `FollowTarget.cs`         - basic camera following
- `InteractableCamera.cs`   - camera interact
- `RoomController.cs`       - call in scene when player interact cam area

- `Creatures` => `AnimationControllers.cs` - Start and End Events
- `Creatures` => `AnimatorHashes.cs` - names to int Hashes
- `Creatures` => `CreatureAnimationTrigger.cs` - triggers for Unity Anim
- `Creatures` => `HeroArmAnimController` - change arm anim state




