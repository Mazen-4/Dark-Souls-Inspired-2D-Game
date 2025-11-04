# Dark-Souls-Inspired-2D-Game
2D platformer game built entirely from scratch using pure C#! Inspired by the atmosphere and mechanics of Dark Souls.
# Dark Souls-Inspired 2D Game
**A 2D action platformer built from scratch in C# without any game engine**

## 🎮 About

This project is a technical exploration of game development fundamentals. Instead of using Unity, Godot, or any game engine, my teammate and I built everything from the ground up using only C# and Windows Forms to understand how game engines work internally.

## ✨ Features

- **Custom Game Engine Architecture**
  - Manual game loop implementation
  - Custom rendering pipeline using GDI+
  - Built without any game engine framework

- **Combat System**
  - Melee sword attacks
  - Ranged moon knight sword attacks
  - Shield blocking mechanic
  - Health system with Dark Souls death sound

- **Player Mechanics**
  - Movement and jumping
  - Sprint ability
  - Attack animations

- **Enemy AI**
  - Multiple enemy types with different behaviors
  - Basic patrol and attack patterns

- **Environmental Systems**
  - Platform-based level design
  - Collectible items
  - Traps and hazards

- **Audio Integration**
  - Background music
  - Iconic Dark Souls "YOU DIED" death sound

## 🛠️ Technical Implementation

**Built Without Game Engines**
- Language: C# 
- IDE: Visual Studio 2022
- Framework: Windows Forms + GDI+ for rendering
- Team: 2 developers
- Physics: Manual collision detection and movement
- No external game libraries

**Key Systems Developed:**
- Custom game loop
- Sprite rendering system
- 2D physics and collision detection
- Input handling
- Audio playback integration
- Entity management

## 🎯 Why We Built This

This project was created to understand game engine architecture from first principles. By building systems manually rather than using an existing engine, we gained hands-on experience with:
- Game loop implementation and timing
- Collision detection systems
- Entity management patterns
- Manual rendering pipelines
- The complexity that game engines abstract away

This experience has given us a deeper understanding of what happens "under the hood" in professional game engines.

## 📦 How to Build & Run

### Requirements
- Visual Studio 2022 or later
- .NET Framework 4.7.2 or higher
- Windows OS

### Steps
1. Clone or download this repository
2. Open `MM_longGame01.sln` in Visual Studio
3. Build the solution (Ctrl+Shift+B)
4. Press F5 to run

### Controls
- **Arrow Keys / WASD**: Move
- **Space**: Jump
- **S**: Sprint
- **Q**: Shield
- **Left Mouse Click**: Melee sword attack
- **Right Mouse Click**: Ranged moon knight sword attack

## 📝 Current State

This is a learning project and contains some bugs and edge cases that are not fully handled. The focus was on understanding game architecture rather than production-level polish.

**Known Limitations:**
- Some edge case bugs present
- Not fully optimized
- Limited sound effects (only background music and death sound)

## 🧠 What We Learned

- How to structure a game without engine assistance
- Manual implementation of game systems (physics, rendering, input)
- The value of game engines and what they provide
- Collision detection mathematics
- The challenges of building game architecture from scratch
- Why professional developers use established engines for production work

## 🚀 Future Context

This project was a learning experience to understand fundamentals. For production games, we now use professional engines like Unity and Unreal, but this hands-on experience with low-level systems has been invaluable for understanding how those engines work internally.

## 📫 Contact

**Mazen Zidan**
- Portfolio: [https://mazen-4.github.io/Portofolio-Website]
- LinkedIn: [https://www.linkedin.com/in/mazen-ismail-282383289]
- Email: mazenzidan@gmail.com

---

*Built to understand game development from first principles. Check out my other projects including [The Ouroboros House](https://mazen-4.itch.io/the-ouroboros-house), a 3D horror game made in Unity for GMTK Game Jam.*
