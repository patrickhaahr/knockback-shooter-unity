## Game Design
- **Genre**: Top-down space shooter (spaceship vs aliens)
- **Combat**: Player shoots missiles at cursor position with knockback opposite to shoot direction
- **Enemy AI**: Closest enemy follows player; others follow closest enemy (performance optimization)
- **Wave System**: Progress by killing enemies (Wave 1: 10 enemies, Wave 2: 20, etc.); enemies get tougher each wave
- **Progression**: Pickup items drop randomly; grant upgrades (health, damage, multi-shot, etc.)
- **Camera**: Follow spaceship player in top-down view

## Build/Test
- This is a Unity 6 (6000.2.8f1) project - open in Unity Editor to build/test
- No automated test framework configured - test manually in Play mode
- Scripts are in `Assets/Scripts/` organized by feature (Camera, Player, Weapons, Enemies)

## Code Style
- **Language**: C# for Unity MonoBehaviour scripts
- **Imports**: `using UnityEngine;` first, then `UnityEngine.*` namespaces, then System
- **Types**: Use explicit types; `[SerializeField] private` for Unity inspector fields
- **Naming**: PascalCase for public/methods, camelCase for private fields/variables
- **Bracing**: Opening brace on same line for methods (Allman style inconsistent - check existing code)
- **Spacing**: Inconsistent (mix of 2/3/4 spaces) - match surrounding code in file being edited
- **Null checks**: Check for null before accessing Unity components (`if (rb != null)`)
- **Lifecycle**: Use `Awake()` for component refs, `Start()` for initialization, `Update()` for input/per-frame
- **Physics**: Use `Rigidbody2D.linearVelocity` (not deprecated `velocity`) for Unity 6
- **Error handling**: Log warnings for missing refs, log errors for critical failures
 - **Debugging**: NEVER use Debug.Log - always choose optimized implementation routes

## Project Structure
- Place new scripts in appropriate `Assets/Scripts/` subdirectory (Camera, Player, Weapons, Enemies, etc.)
- Prefabs go in `Assets/Prefabs/` with matching subdirectories
- Use `[SerializeField]` for prefab references and tunable parameters
