# Pirate‑Adventure‑2D

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
## A small 2D platformer prototype in Unity about a cheeky pirate, treasure, and trouble at sea.

### ✨ Features

- ⚓️ Tight 2D platformer controls (run, jump, coyote time, variable jump)
- 🗡️ Melee & ranged attack foundation - in progress
- 🧠 State‑based player/enemy logic (FSM)
- 🌴 Parallax backgrounds, tileset‑based levels
- 🎵 Basic SFX/music hooks - in future
- 🧰 Simple save/checkpoint system (stub) - in future

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

### 🗂️ Project Structure / Scripts
PlayerController.cs — движение, прыжок, стен‑джамп/койот‑тайм

PlayerStateMachine/ — состояния Idle/Run/Jump/Attack/Dash

EnemyPatrol.cs — патруль/агро на игрока

GameManager.cs — управление сессией, пауза, рестарт

ParallaxController.cs — параллакс‑слои


```sh
Pirate-Adventures-2D/
│── PlayerInput/      # New Input System   
│── Resources/
    | --          
│── Fabrics/        
│── Systems/        
│── Venicals/       
│── States/         
│── Program.cs      
│── README.md      
```







  

