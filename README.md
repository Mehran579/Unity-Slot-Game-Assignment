# Unity Slot Game Assignment

A 3-reel slot machine built in Unity as a technical assignment. Players bet points, spin three reels, and win a payout when all three reels land on the same symbol, with a jackpot multiplier for the top symbol.

## Gameplay

- Start with a fixed points balance (1000 by default).
- Increase/decrease the bet in steps of 10 points, bounded by your current balance.
- Hit **Spin** to spend the bet and roll all three reels.
- If all three reels stop on the same symbol, you win `bet × multiplier` for that symbol (multipliers are configured per symbol, with the highest-index symbol paying out as the jackpot).
- A restart button reloads the scene and resets the game.

## How it's built

The project is organized as a small set of focused, single-responsibility scripts under `Assets/Scripts`:

| Script | Responsibility |
|---|---|
| `SlotMachine.cs` | Central controller and UI manager. Wires up buttons, tracks balance/bet/wins, and decides win/lose after the reels stop. |
| `SlotReel.cs` | Drives a single reel: scrolls its child symbols on a loop, picks a random target symbol per spin, and snaps to a stop when it reaches that target. Fires an `OnStopped` event so the controller knows when it's safe to react. |
| `SlotRandomNumberGenerator.cs` | A small static helper that generates spin outcomes using `System.Security.Cryptography.RandomNumberGenerator` instead of `UnityEngine.Random`, for higher-quality randomness. |
| `SoundManager.cs` | A singleton audio manager (spin loop, reel-stop, win, and jackpot sound effects). |

**Reel animation approach:** each reel is a set of child symbol transforms that scroll vertically and wrap around using `Mathf.Repeat`, so a small, fixed number of symbols can simulate an endless reel. When the spin timer elapses, the reel waits until the chosen target symbol is near its resting position, then snaps it exactly into place — giving a natural-looking spin with a clean stop rather than an abrupt cut.

**Win detection:** decoupled from the spin animation via a C# event (`SlotReel.OnStopped`). The controller only checks for a win once the last reel reports that it has physically stopped, so results are only ever evaluated visually after the animation resolves.

## Project structure

```
Assets/
  Scripts/     Core game logic (see table above)
  Prefabs/     Canvas prefab for the game UI
  Scenes/      SampleScene — the single playable scene
  UI/          Slot machine art (reels, symbols, buttons, backgrounds)
  Sounds/      Spin, stop, win, and jackpot audio clips
  Settings/    URP render pipeline settings
```

## Requirements

- Unity **6000.3.9f1** (Unity 6) or compatible — see `ProjectSettings/ProjectVersion.txt`.
- Universal Render Pipeline (URP), Input System, and TextMesh Pro packages (restored automatically via the Package Manager from `Packages/manifest.json`).

## Running the project

1. Clone the repo.
2. Open it with Unity Hub using the Unity version listed above (or let Unity Hub prompt you to install it).
3. Open `Assets/Scenes/SampleScene.unity`.
4. Press Play. Use the on-screen bet +/- buttons and the Spin button to play.

## Notes

- The RNG is deliberately cryptographically-seeded rather than using `UnityEngine.Random`, favoring unbiased outcomes over deterministic/seedable testing.
- Jackpot multipliers are exposed as an array on `SlotMachine` in the Inspector, so payout tuning doesn't require touching code.
- The reel symbol count is not hardcoded — adding more child symbols under a reel automatically expands the possible outcomes for that reel.
