Install Packages:
Windows -> Package Manager -> Unity Registry -> 
- Netcode for GameObjects
- Multiplayer Tools

Network Manager ()Only one Network Manager at all):
- Hirachy -> Right Click -> Create Empty -> NetworkManager (Inspector -> Right click on Transform -> Reset)
- Inspector -> Add Component -> Netcode -> Network Manager
- Network Transport -> Select -> Unity Transport (This is for handling sending and resiciving packets)

Create a simple Character:
- Hirachy -> Right Click -> Create Empty -> Player (Inspector -> Right click on Transform -> Reset)
- Hirachy -> Right click on Player -> 3D Object -> Capsule (Inspector -> Transform -> Position -> 0, 1, 0)
- Player (Not Capsule) -> Add Component -> Netcode -> Network Object
// This will be our player object, so we need to use it as a prefab object
- Drag Player into Assets in Project Window and delete the Player in Hirachy
- Hirachy -> Network Manager -> Inspector -> Drag the player prefag from assets into the Player Prefab field
// Every play who conntect the game will have a copy of this prefab. This is not mandatory, in some Games, e.g. a strategy, the player will not have a character.
// For the first test, we can click on the play button and then on the host button on then network manager. This will start a host and spwan a player object on the view.
- Inspector -> Add Componet -> Netcode -> Network Transform (This will sync the position of the player object, only for owner use)
- Inspector -> Add Componet -> Netcode -> Client Network Transform (For owner and Client)

(Optional) Create UI-Buttons for Server and Client:
- Hirachy -> Right Click -> UI -> Canvas
- In Canvas -> Create Empty -> NetworkManagerUI
- Strech (buttom right) -> left,top, Pos z, right, bottom all 0
- Right Click on NetworkManagerUI -> UI -> Button TextMash Pro (If you get ask to install TextMesh Pro, click on install, delete and create the button again) call it ServerBtn
- Edit the Text of the Button (Inside the Button hirachy) to Server (Position Strecht top right)
- Copy the Button two times and rename them to ClientBtn and HostBtn

Scripting for UI-Buttons:
- New C# Script -> Name it NetworkManagerUI in Assets -> Drag the Script into the NetworkManagerUI (see code in Assets -> Scripts)
- Add the Script to the NetworkManagerUI and drag the buttons into the fields in the script component in the inspector
- Change Log Level friom NetworkManager (not NetworkManagerUI) to developer
  
Scripting for Player:
- New C# Script -> Name it PlayerNetwork -> Drag the Script into the Player (see code in Assets -> Scripts)C
  
Client Trust, both can move:
Windows -> Package Manager -> Add from git URL.. ->
https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop.git?path=/Packages/com.unity.multiplayer.samples.coop#main