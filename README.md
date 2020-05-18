![Hero-Sage Logo](/DGD-Heroes/Assets/Graphics/Objects/gameBannerSlaphScreen.png)

#### External resources
- Hero: https://free-game-assets.itch.io/free-tiny-hero-sprites-pixel-art
- Coins: https://laredgames.itch.io/gems-coins-free
- Objects: https://alexs-assets.itch.io/16x16-rpg-item-pack
- Fireball: https://stealthix.itch.io/animated-fires
- Enemies: https://lionheart963.itch.io/free-platformer-assets
- Heart: https://fliflifly.itch.io/hearts-and-health-bar
- Font: https://www.dafont.com/thaleahfat.font
- Background & Tileset: https://mamanezakon.itch.io/forest-tileset
- Soundtrack: https://www.youtube.com/watch?v=oMgQJEcVToY
- Sound / FX: https://harvey656.itch.io/8-bit-game-sound-effects-collection

#### Features and implementations
| Feature | Code reference |
|-------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Splash screen | Implemented with Unity editor |
| Game icon | Implemented with Unity editor |
| Sound/Music | Main soundtrack in Menu, played with [SoundManager](/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs) |
| Controls - KeyBinding | KeyBinding allowed by the main Menu [GameInputManager](/DGD-Heroes/Assets/Scripts/Manager/GameInputManager.cs), [ChangeKeyScript](/DGD-Heroes/Assets/Scripts/UI/ChangeKeyScript.cs) |
| Credits Screen | Static page, reachable by the main Menu |
| Scoreboard - Sorted | The scoreboard is reachable by the main Menu or when the game is over. [HightScoreTable](/DGD-Heroes/Assets/Scripts/UI/HightScoreTable.cs), [ScoreManager](/DGD-Heroes/Assets/Scripts/Static/ScoreManager.cs) |
| Tutorial | 3 Static scenes |
| Score | The score is stored and calculated in static class [PlayerStats](/DGD-Heroes/Assets/Scripts/Static/PlayerStats.cs) |
| PowerUPs | There are three PowerUPs: Attack, Defense, DoubleCoins. [PowerUP](/DGD-Heroes/Assets/Scripts/Objects/PowerUP.cs) |
| Time-Base | [TimerCountdown.cs](/DGD-Heroes/Assets/Scripts/UI/TimerCountdown.cs) |
| Enemy | Goblin, Slime, RedBat [Enemies](/DGD-Heroes/Assets/Scripts/Enemies) |
| Multiple levels | There are two levels |
| Basic AI | The enemy follow the player if is near by [Enemy - Move](/DGD-Heroes/Assets/Scripts/Enemies/Enemy.cs), [IsNearOther2D](/DGD-Heroes/Assets/Scripts/Static/Extension.cs) |
| PlayerPrefs | Store the custom controls and score [ScoreManager](/DGD-Heroes/Assets/Scripts/Static/ScoreManager.cs), [ChangeKeyScript](/DGD-Heroes/Assets/Scripts/UI/ChangeKeyScript.cs) |
| Singleton | [GameInputManager](/DGD-Heroes/Assets/Scripts//Manager/GameInputManager.cs), [GameSingletonUI](/DGD-Heroes/Assets/Scripts/UI/GameSingletonUI.cs), [TimerCountdown.cs](/DGD-Heroes/Assets/Scripts/UI/TimerCountdown.cs), [SoundManager](/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs), [CameraBoundsManager](/DGD-Heroes/Assets/Scripts/Manager/CameraBoundsManager.cs), [SceneController](/DGD-Heroes/Assets/Scripts/Manager/SceneController.cs), [~~CoinsManager~~](/DGD-Heroes/Assets/Scripts/Manager/CoinsManager.cs) |
| Coorutines | [UnlockMove](/DGD-Heroes/Assets/Scripts/Hero/EdgeCheck.cs), [animationState - Player](/DGD-Heroes/Assets/Scripts/Hero/PlayerController.cs), [SpawnCoins, DeactivateDistanceCoins](/DGD-Heroes/Assets/Scripts/Manager/CoinsSpawnManager.cs), [WaitForKey, AssignKey, DotAnimation](/DGD-Heroes/Assets/Scripts/UI/ChangeKeyScript.cs), [TimerTake](/DGD-Heroes/Assets/Scripts/UI/TimerCountdown.cs) |
| Enums | [Direction](/DGD-Heroes/Assets/Scripts/Hero/PlayerController.cs), [PowerUPs](/DGD-Heroes/Assets/Scripts/Objects/PowerUP.cs) |
| Static class | [Extension](/DGD-Heroes/Assets/Scripts/Static/Extension.cs), [PlayerStats](/DGD-Heroes/Assets/Scripts/Static/PlayerStats.cs), [ScoreManager](/DGD-Heroes/Assets/Scripts/Static/ScoreManager.cs) |
| Inheritance | [Enemy](/DGD-Heroes/Assets/Scripts/Enemies/Enemy.cs) > [FlyingEnemy](/DGD-Heroes/Assets/Scripts/Enemies/FlyingEnemy.cs) > [GroundEnemy](/DGD-Heroes/Assets/Scripts/Enemies/GroundEnemy.cs) |
| Extension Methods | [Spawn, Flip2D, IsNearOther2D, ChangeAlpha, Capitalize, RepeatForLoop, SetNameAndScore, SortListByScore](/DGD-Heroes/Assets/Scripts/Static/Extension.cs) |
| Animations | Hero: Idle, Jump, Hurt, Death. Coins, Enemy: Hurt, Idle |
| Soundtrack | Main soundtrack played with singleton [SoundManager](/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs) |
| Sounds / FX | Coins collect, player hurt, enemy hurt, player jump, powerup [SoundManager](/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs) |
| Raycast | Fireball collision, enemy detect [SpecialAttack](/DGD-Heroes/Assets/Scripts/Hero/SpecialAttack.cs)  |
| User interface | Hero, enemy (Goblin, Slime, RedBat), enemy health bar, tiles, tree, powerUPs (Attack, Defense, DoubleCoins), parallax background, coins, custom ui |
| Particles system | Glow powerUPs, fireball explosion, player jump dust, tree around light |
