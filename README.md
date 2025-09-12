
# 🏴‍☠️ Pirate Adventure 2D

> A small 2D platformer prototype in **Unity** about a cheeky pirate, treasure, and trouble at sea.

[![Unity](https://img.shields.io/badge/Made%20with-Unity-000?logo=unity)](#)
[![C%23](https://img.shields.io/badge/Code-C%23-239120?logo=csharp&logoColor=white)](#)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Branches](https://img.shields.io/badge/branch-HomeWork--11-blue)](#)
[![Issues](https://img.shields.io/github/issues/vadimvatsenko/Pirate-Adventure-2D)](#)

<p align="center">
  <img src="img/cover.png" alt="Cover" width="760"/><br/>
  <i>Replace with your own screenshots / GIFs</i>
</p>

---

## Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Screenshots](#screenshots)
- [Controls](#controls)
- [Project Structure](#project-structure)
- [Code Highlights](#code-highlights)
- [Getting Started](#getting-started)
- [Build](#build)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

---

## Features
- ⚓️ Tight 2D platformer controls *(run, jump, coyote time, variable jump)*
- 🗡️ Melee & ranged attack foundation *(WIP)*
- 🧠 State‑based logic *(FSM for player/enemies)*
- 🌆 Parallax backgrounds, tileset‑based levels
- 🔉 Basic SFX/music hooks *(planned)*
- 💾 Simple save/checkpoint system *(planned)*

---

## Tech Stack
- **Unity** LTS (recommend **2022.3.x** or project version)
- **C#** scripts
- **Unity 2D** tools (Sprite, Tilemap, Rule Tiles, etc.)
- **Cinemachine** (camera)
- **Input System** (New Input System)

---

## Screenshots
> Put your real media in the **img/** folder and link here.

| Gameplay                              |
|---------------------------------------|
| ![Gameplay](img/PirateGameplay.gif)   |



---

## Controls
| Action            | Keyboard / Mouse |
|-------------------|------------------|
| Move              | **A / D**        |
| Jump              | **Space**        |
| Attack            | **Mouse 0**      |
| Interact          | **R**            |
| Pause             | **Esc**          |

---

## Project Structure
```
Pirate-Adventure-2D/
│── PlayerInput/             # New Input System
│── Resources/               # Game Resources
│   ├── Animations           # Animation Files
│   ├── Camera               # Camera Blends
│   ├── Fonts                # In‑game Fonts
│   ├── Gradient             # Color Gradients
│   ├── Materials            # Materials
│   ├── Prefabs              # Game Prefabs
│   ├── RenderPipeLine       # Lights
│   ├── Sprites              # Sprites
│   └── TilePalette          # Tiles
│── Scenes/                  # Game Scenes
│── Scripts/                 # C# code
└── README.md
```

---

## 🧠 Code Highlights 

### General
- **`ParticleSystemSwitcher.cs`** — switches between different particle systems (effects).

---

### 📷 Cam (Camera)
- `CameraBoundsSwitcher.cs` — constrains the camera within room bounds.
- `FollowTarget.cs` — simple follow camera (legacy).
- `InteractableCamera.cs` — temporary events like focus or zoom.
- `RoomCamera.cs` — old camera switching logic.
- `RoomController.cs` — attached to a room prefab, manages which camera is active.

---

### 🧩 Components
- `ArmPlayerComponent.cs` — handles the player’s arm logic.
- `CoinsComponent.cs` — processes coins (collecting, counters).
- `DestroyGameObjectComponent.cs` — destroys an object under certain conditions.
- `Directions.cs` — enums/utilities for directions.
- `ExitLevelComponent.cs` — triggers level completion.
- `FallingComponent.cs` — makes an object fall.
- `FallingPlatformComponent.cs` — collapsible platform logic.
- `FloodWater.cs` — rising water hazard.
- `HeroInputReader.cs` — wrapper for Unity Input System.
- `HiddenDoor.cs` — hidden door/portal logic.
- `InteractableComponent.cs` — base interactable component.
- `LayerCheck.cs` — layer checking helper.
- `OutOfLevel.cs` — handles leaving the playable area.
- `RollingItemsComponent.cs` — rolling items on the ground.
- `ScoreComponent.cs` — scoring system.
- `TeleportComponent.cs` — teleports player or objects.

#### Dropper
- `DroppedObjectEntry.cs` — loot entry data.
- `DropperDirection.cs` — drop direction logic.
- `GameObjectDropper.cs` — core drop system.

#### EnterCollisionComponents
- `EnterCollisionComponent*.cs` — reusable trigger scripts reacting to **OnTriggerEnter**, used for pickups, events, and doors.

#### HealthComponentFolder
- `HealthComponent.cs` — base health logic.
- `HealthModifier.cs`, `OtherHealthModifier.cs` — apply health effects.
- `IHealthComponent.cs` — health interface.
- `PlayerHealthComponent.cs` — player-specific health.

#### Parallax
- `ParallaxLayer.cs` — background/foreground parallax layers.
- `ParallaxClouds.cs` — preset cloud parallax.

#### Spawn
- `ProbabilityDropComponent.cs` — weighted random logic.
- `SpawnComponent.cs` — spawns prefabs by ID.
- `SpawnListComponent.cs` — spawns groups of prefabs.

#### Teleport
- `TeleportEvent.cs` — teleport event hook.
- `TeleportTransitionEffect.cs` — teleport transition visuals.

#### TileMaps
- `TileMapCleaner.cs` — cleans tilemaps.

#### Triggers
- `TriggerClimb.cs` — climbing trigger.

#### UI
- `HealthUI.cs` — health display UI.

#### VirtualCamera
- `EnterInCameraEvent.cs`, `EnterInCameraZone.cs` — camera zone events.
- `TempCameraFollow.cs` — temporary follow logic.
- `VirtualCameraController.cs` — main virtual camera controller.

---

### 🎮 Controllers
- **Cheats/**
    - `CheatController.cs` — manages cheat commands.
    - `CheatItem.cs` — defines a cheat entry.
- **PlayerControllers/**
    - `CoinsController.cs` — coin handling for player.
    - `HealthBarController.cs` — health bar UI.
    - `IsPlayerWithInput.cs` — checks if the player has active input.

---

### 🐉 Creatures
- **Creature_OLD.cs** — legacy creature logic.

**AnimationControllers**
- `AnimationEventReceiver.cs` — receives Animation Events.
- `AnimatorHashes.cs` — central Animator parameter hashes.
- `CreatureAnimationTrigger.cs` — animation triggers.
- `CreatureArmAnimController.cs` — arm controller for creatures.
- `HeroArmAnimController.cs` — arm controller for the hero.

**CreaturesCollisions**
- `CombatCollisionCheck.cs` — combat hitbox detection.
- `CreatureCollisionCheck.cs` — base collision checks.
- `EnemyCollisionCheck.cs` — enemy-specific collisions.
- `HeroCollisionCheck.cs` — hero-specific collisions.

**CreaturesHealth**
- `CreatureHealth.cs` — base health logic.
- `EnemyHealth.cs` — enemy health.
- `HeroHealth.cs` — hero health.

**CreaturesStateMachine**
- Multiple `CreatureBehaviour_*.cs` — states like Idle, Walk, Jump, Attack, Dead, Hurt, etc.
- **Enemies/** — AI states (patrol, chase, attack).
- **Player/** — player states (Grounded, Jump, Fall, Attack, etc.).

**CreaturesVFX**
- `CreatureVFX.cs` — VFX controller for creatures.
- `ParticleEntry.cs` — particle entry data.
- `ParticleType.cs` — particle type definitions.

**Interfaces**
- `IFacingDirection.cs` — facing direction interface.

**Settings**
- `CreatureSettings.cs` — creature settings (speed, damage, etc.).

---

### 🛠 Editor
- `GameObjectDropperEditor.cs` — custom inspector for the dropper.

---

### 📋 GameManagerInfo
- `GameManager.cs` — global game manager.
- `GameSession.cs` — player session data.
- `LevelController.cs` — level flow controller.
- `PlayerData.cs` — persistent player data.

---

### ⚓ Items
- `Barrel.cs`, `Sail.cs`, `Ship.cs` — ship-related items.
- `DeadZone.cs` — death zone.

**Candles/**
- `CandleFlicker.cs`, `CandleLightController.cs` — candle lights.

**Coins/**
- `Coin.cs`, `CoinPickUpVfx.cs`, `CoinType.cs` — coins.

**GatesSwitchers/**
- `GateSwitchDirection.cs`, `OnlyOpenOrClose.cs`, `OpenGatesSwitcher.cs` — gate logic.

**Traps/**
- `ITraps.cs` — trap interface.
- `Spike.cs`, `SpikesController.cs` — spikes logic.
- `TrapsDamageTrigger.cs` — applies damage.
- **Saw/**: `SawBackForward.cs`, `TrapSaw.cs` — saw traps.

---

### 🎞 SpriteAnimators
- `HandleAnimationClip.cs`, `HandleSpriteAnimator.cs` — sprite animation controllers.
- `SpriteAlphaPulse.cs` — alpha blinking effect.

**AnimationControllers/**
- `HelmAnimController.cs`, `PlayerAnimController.cs`, `ShipAnimController.cs`, `SimpleAnimController.cs` — animation controllers.

**AnimationTypes/**
- `HelmAnimation.cs`, `PlayerAnimation.cs`, `ShipAnimation.cs` — animation type definitions.

**NewSpriteAnimator/**
- `AnimationController.cs`, `SpriteAnimator.cs` — new sprite animation system.

---

### 🔧 Utils
- `DontDestroy.cs` — makes object persistent across scenes.
- `HandlesUtils.cs` — gizmos/handles helpers.
- `RandomSpawner.cs` — spawns random objects.
- `Timer.cs` — simple timer utility.

---

## Getting Started
### Prerequisites
- Unity **2024.x LTS** or newer *(open in the version the project was created with)*

### Clone
```bash
git clone https://github.com/vadimvatsenko/Pirate-Adventure-2D.git
git checkout HomeWork-11
```

### Open
Open the folder in **Unity Hub**, select the correct editor version, let it import, then **Play**.

---

## Build
Unity → **File → Build Settings** → choose platform (Windows/macOS/Linux) → **Build**.
Add main scenes to **Scenes In Build** before building.

---

## Roadmap
- [ ] Checkpoints & simple save system
- [ ] Enemies AI (patrol, chase, attack)
- [ ] Weapons & ranged combat
- [ ] Boss fights
- [ ] Audio pass (SFX/music)
- [ ] CI builds (GitHub Actions)

---


