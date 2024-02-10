# Magic Survivalist
### Please be attentive and run the project as follows
You need to find the Main scene in the project and open it. There is no camera on scene and that's normal. Just run the game.

![image](https://github.com/GrapesOne/Magic-Survivalist/assets/35427288/4d290e23-83e3-408a-bb6b-365c035fdda6)

The settings of all entities in the game are stored in the path Assets/Resources/ScriptableObjects/

![image](https://github.com/GrapesOne/Magic-Survivalist/assets/35427288/1ee045ee-cffe-43c2-bbb0-ef4cf1c3948c)

Damage dealt is displayed in the console

![image](https://github.com/GrapesOne/Magic-Survivalist/assets/35427288/da74b555-0c86-4909-9d22-0bf910ced94a)

## Unrealized plans

- Add pooling of all visual controllers. Should be written in SpawnSystem.
- Display the health of the player and enemies in the game above their heads. It need to create a separate
canvas. When registering an entity, a link will be sent to the health tracking system. If the entity dies, the
registration will be cancelled.
- Change spawn. In this version, player creation is difficult to comprehend. Initialization occurs in different
places and is a weakness. Disparate initialization should be consolidated, and creation should also be moved to
factories.
