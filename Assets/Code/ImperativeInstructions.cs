using Code.Combat.Data;
using Code.Core;
using Code.Core.StateManagement;
using UnityEngine;

namespace Code {

    public static class ImperativeInstructions {
        public static void RunGame() {
            var gameState = Main.Instance.GameStateManager.CurrentGameState as GameplayState;
            var battleSystem = gameState.BattleSystem;
            var spawnSystem = battleSystem.SpawnSystem;
            var playerController = spawnSystem.SpawnUnitController(1000, UnitType.player, new Vector3(0, 0, 0), Quaternion.identity);
            var playerEntity = spawnSystem.SpawnPlayerEntity(1000);
            EventBus.RegisterControllerInBattleArea?.Invoke(playerController);

            var standardProjectileData = battleSystem.CollectionAggregator.GetCurrentProjectileOrFirst();
            playerEntity.SetProjectileData(standardProjectileData);
        }
    }

}