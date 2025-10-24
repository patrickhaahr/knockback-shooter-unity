# Space Shooter - Top-Down Wave Survival

A fast-paced top-down space shooter where you pilot a spaceship against endless waves of alien enemies. Master the knockback mechanics, survive increasingly difficult waves, and defeat powerful bosses to claim upgrades!

## 📸 Screenshots & Media

> **Note:** Add your gameplay screenshots and GIFs here!

```
[Gameplay Screenshot 1]
[Gameplay Screenshot 2]
[Boss Battle GIF]
[Pickup System GIF]
```

---

## 🎮 How to Play

### Controls
- **Mouse Movement**: Aim your spaceship (rotates toward cursor)
- **Left Click**: Fire missile at cursor position
- **Movement**: Shooting creates knockback - use it strategically to dodge!

### Objective
Survive endless waves of enemies, defeat bosses, and collect upgrades to become stronger.

---

## 🚀 Game Mechanics

### Combat System
- **Cursor-Based Shooting**: Your spaceship always aims at the mouse cursor
- **Knockback Movement**: Each shot pushes you in the opposite direction - master this for mobility!
- **Ammo System**: 
  - Start with 15 max ammo
  - Regenerates 2 ammo every 2 seconds
  - Each kill grants +1 bonus ammo
- **Fire Rate**: 0.1 second cooldown between shots
- **Base Damage**: 25 damage per missile

### Player Stats
- **Health**: 100 HP (increases with pickups)
- **Collision Damage**: Touching enemies deals 10 damage
- **Kill Rewards**: 
  - +1 ammo per kill
  - +10 health per kill

### Enemy Types

#### 🔵 Standard Enemies
- **Behavior**: Direct pursuit AI with separation (prevents clumping)
- **Health**: 50 HP (scales with wave difficulty)
- **Movement**: 6 units/second base speed (increases each wave)
- **Damage**: 10 damage on collision

#### 🔴 Enemy Shooters (Guards)
- **Behavior**: Static turrets that shoot missiles at player
- **Spawn**: Appear near pickups as guards
- **Fire Rate**: Every 2 seconds
- **Missile Speed**: 5 units/second
- **Threat**: Ranged attackers - prioritize or avoid!

#### 👑 Boss Enemies
- **Trigger**: Spawns every 10 kills
- **Health**: Massive HP pool that scales exponentially with each defeat
- **Speed**: Slower but relentless pursuit
- **Attack Patterns** (cycles through 3):
  1. **Burst Fire**: 5 rapid missiles aimed at player (0.2s between shots)
  2. **Spread Shot**: 7-way spread of missiles in a cone (45° angle)
  3. **Minion Spawn**: Summons 3 standard enemies around itself
- **Difficulty Scaling**: Each subsequent boss is 2x stronger in all stats
- **Defeat Rewards**: 10 pickups spawn in a circle around defeat location

### Boss Battle Mechanics
- When boss spawns:
  - Screen shakes for dramatic effect
  - All standard enemies despawn
  - Normal wave spawning pauses
- After boss defeat:
  - 5 second pickup collection window
  - Standard enemies become 2x harder permanently
  - Wave system resumes with increased difficulty

---

## 📦 Pickup System

Pickups spawn randomly with a 60% base chance after killing 2+ enemies. If no pickup spawns, chance increases by 5% per kill until one drops.

### Pickup Types (Random)
| Pickup | Effect | Description |
|--------|--------|-------------|
| 💚 **Health** | +20 Max HP | Permanently increases health capacity and heals |
| ⚔️ **Damage** | +5 Damage | Increases missile damage |
| 🎯 **Ammo** | Full Reload + 5 Max | Refills ammo and increases capacity by 5 |
| 💥 **Knockback** | Enhanced Power | +0.5 distance, +0.3 speed to knockback |

---

## 🌊 Wave System

### Wave Progression
- **Wave 1**: 10 enemies
- **Wave 2**: 20 enemies
- **Wave 3**: 30 enemies
- Pattern continues: **+10 enemies per wave**

### Difficulty Scaling
- **Enemy Health**: Increases each wave
- **Enemy Speed**: Increases each wave
- **Boss Multiplier**: 2x stats per defeat
- **Post-Boss Scaling**: All enemies permanently 2x harder after each boss defeat

### Spawn Mechanics
- **Spawn Rate**: 1 enemy every 2 seconds
- **Spawn Distance**: 10-15 units from player (off-screen)
- **Wave Delay**: 5 second break between waves

---

## 🎯 Features

### Core Gameplay
- ✅ Cursor-based aiming and shooting
- ✅ Knockback movement system for tactical positioning
- ✅ Ammo management with auto-regeneration
- ✅ Camera follows player ship

### Enemy AI
- ✅ Smart pursuit behavior with separation (no clumping)
- ✅ Ranged turret enemies that guard pickups
- ✅ Boss with 3 unique attack patterns
- ✅ Exponential difficulty scaling

### Progression Systems
- ✅ Wave-based survival with increasing difficulty
- ✅ Boss battles every 10 kills
- ✅ Random pickup drops with guaranteed pity system
- ✅ Kill rewards (ammo + health)
- ✅ 4 different upgrade types

### Polish & UI
- ✅ Real-time health bar
- ✅ Ammo counter display
- ✅ Kill counter
- ✅ Pickup notification system
- ✅ Camera shake on boss spawn
- ✅ Game over screen
- ✅ Start menu

---

## 🛠️ Technical Details

### Built With
- **Engine**: Unity 6 (6000.2.8f1)
- **Rendering**: Universal Render Pipeline (URP)
- **Input**: Unity Input System (new input system)
- **UI**: UI Toolkit (UXML)
- **Language**: C#

### Project Structure
```
Assets/
├── Scripts/
│   ├── Player/         # Player controller, health, combat
│   ├── Enemies/        # Enemy AI, health, boss behavior
│   ├── Weapons/        # Missile projectiles (player & enemy)
│   ├── Managers/       # Game, wave, spawn, boss battle systems
│   ├── Pickups/        # Pickup items and effects
│   ├── UI/             # Health bars, ammo, notifications, menus
│   └── Camera/         # Camera follow and shake effects
├── Prefabs/            # Reusable game objects
├── Sprites/            # 2D artwork (ships, enemies, missiles)
├── Scenes/             # Game and start menu scenes
└── UI/                 # UXML UI layouts
```

### Build & Test
This is a Unity project - no command-line build tools configured.

**To Play:**
1. Open project in Unity 6 (6000.2.8f1)
2. Open `Assets/Scenes/StartScene.unity`
3. Press Play in Unity Editor
4. Click "Start Game" to begin

**Testing:**
- Manual playtesting in Unity Editor
- No automated test framework configured

---

## 🎮 Gameplay Tips

1. **Use Knockback Strategically**: Your shots push you backward - use this for quick dodges and repositioning
2. **Manage Ammo**: Don't spam shots - you only regenerate 2 ammo every 2 seconds
3. **Kill Rewards Add Up**: Each kill gives +1 ammo and +10 health - stay aggressive!
4. **Prioritize Pickups**: They're guarded by shooters, but the upgrades are worth it
5. **Boss Pattern Recognition**: Learn the 3 attack patterns and position accordingly
6. **Spread Shot Danger**: Boss spread shots are hardest to dodge - keep distance
7. **Wave Breaks**: Use the 5-second pause between waves to collect pickups safely
8. **Post-Boss Windows**: 5 seconds of safety after boss defeat - grab those 10 pickups!

---

## 🎨 Game Design Philosophy

### Simple, Performant AI
Enemies use direct pursuit with separation behavior - clean, performant, and creates natural enemy formations without complex pathfinding.

### Knockback as Movement
The unique movement system creates interesting decisions: do you shoot to damage, or shoot to reposition? Mobility and offense are the same action.

### Risk-Reward Pickups
Pickups are guarded by dangerous turrets, creating meaningful choices about when to commit to upgrades.

### Escalating Challenge
Multiple difficulty layers (wave scaling, boss fights, post-boss multipliers) ensure the game stays challenging no matter how strong you become.

---

## 📝 License

This project is available for educational and portfolio purposes.

---

## 🚧 Future Ideas

- Additional enemy types with unique behaviors
- More boss attack patterns
- Power-up combinations/synergies
- Leaderboard system
- Audio and sound effects
- Particle effects for impacts
- Multiple playable ships

---

**Developed with Unity 6**
