![Hero-Sage Logo](/DGD-Heroes/Assets/Graphics/Objects/gameBannerSlaphScreen.png)

#### External resources
- Hero: https://free-game-assets.itch.io/free-tiny-hero-sprites-pixel-art
- Coins: https://laredgames.itch.io/gems-coins-free
- Objects: https://alexs-assets.itch.io/16x16-rpg-item-pack
- Fireball: https://stealthix.itch.io/animated-fires
- Enemies: https://lionheart963.itch.io/free-platformer-assets
- Background & Tileset: https://mamanezakon.itch.io/forest-tileset
- Soundtrack: https://www.youtube.com/watch?v=oMgQJEcVToY
- Sound / FX: https://harvey656.itch.io/8-bit-game-sound-effects-collection

#### Features and implementations
| Feature | Code reference |
|-------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Splash screen | Implemented with Unity editor |
| Game icon | Implemented with Unity editor |
| Sound/Music | Main soundtrack in Menu, played with [SoundManager](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs) |
| Controls - KeyBinding | KeyBinding allowed by the main Menu [GameInputManager](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/GameInputManager.cs), [ChangeKeyScript](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/ChangeKeyScript.cs) |
| Credits Screen | Static page, reachable by the main Menu |
| Scoreboard - Sort | The scoreboard is reachable by the main Menu or when the game is over. [HightScoreTable](Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/HightScoreTable.cs), [ScoreManager](Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/ScoreManager.cs) |
| Tutorial - Static scene | Static scene |
| Score | The score is stored and calculated in static class [PlayerStats](Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/PlayerStats.cs) |
| PowerUPs | There are tree PowerUPs: Attack, Defense, DoubleCoins. [PowerUP](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Objects/PowerUP.cs) |
| Time-Base | [TimerCountdown.cs](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/TimerCountdown.cs) |
| Enemy / Challenge | Goblin, Slime, RedBat [Enemis](/Hero-Saga/tree/master/DGD-Heroes/Assets/Scripts/Enemies) |
| Multiple levels | There are two levels |
| Basic AI | The enemy follow the player is near by [Enemy - Move](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Enemies/Enemy.cs), [IsNearOther2D](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/Extension.cs) |
| PlayerPrefs | Store the custom controls and score [ScoreManager](/Tkd-Alex/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/ScoreManager.cs), [ChangeKeyScript](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/ChangeKeyScript.cs) |
| Singleton | [GameSingletonUI](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/GameSingletonUI.cs), [TimerCountdown.cs](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/TimerCountdown.cs), [SoundManager](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs), [CameraBoundsManager](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/CameraBoundsManager.cs), [SceneController](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/SceneController.cs), [~~CoinsManager~~](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/CoinsManager.cs) |
| Coorutines | [unlockMove](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Hero/EdgeCheck.cs), [animationState - Player](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Hero/PlayerController.cs), [SpawnCoins, DeactivateDistanceCoins](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/CoinsSpawnManager.cs), [WaitForKey, AssignKey, DotAnimation](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/ChangeKeyScript.cs), [TimerTake](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/UI/TimerCountdown.cs) |
| Enums | [Direction](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Hero/PlayerController.cs), [PowerUPs](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Objects/PowerUP.cs) |
| Static class | [Extension](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/Extension.cs), [PlayerStats](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/PlayerStats.cs), [ScoreManager](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/ScoreManager.cs) |
| Inheritance | [Enemy](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Enemies/Enemy.cs) > [FlyingEnemy](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Enemies/FlyingEnemy.cs) > [GroundEnemy](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Enemies/GroundEnemy.cs) |
| Extension Methods | [Spawn, Flip2D, IsNearOther2D, ChangeAlpha, Capitalize, RepeatForLoop, SetNameAndScore](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Static/Extension.cs) |
| Animations | Hero: Idle, Jump, Hurt, Death. Coins, Enemy: Hurt, Idle |
| Soundtrack | Main soundtrack played with singleton [SoundManager](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs) |
| Other sounds / FX | Coins collect, player hurt, enemy hurt, player jump [SoundManager](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Manager/SoundManager.cs) |
| Raycast | Fireball collision, enemy detect [SpecialAttack](/Hero-Saga/blob/master/DGD-Heroes/Assets/Scripts/Hero/SpecialAttack.cs)  |
| User interface | Hero, enemy (Goblin, Slime, RedBat), tiles, tree, powerUPs (Attack, Defense, DoubleCoins), parallax background, coins, custom ui |
| Particles system | Glow powerUPs, fireball explosion, player jump dust, tree around light |
