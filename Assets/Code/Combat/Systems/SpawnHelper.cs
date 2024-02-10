using System.Collections;
using Code.Combat.Units.Entities;
using Code.Core;
using UnityEngine;

namespace Code.Combat.Systems {

    public class SpawnHelper {
        private SpawnSystem _spawnSystem;
        private BattleSystem _battleSystem;
        private IEnemyCollectionAggregator _enemyCollectionAggregator;

        private const int MaxUnits = 10;
        private const float SpawnDelay = 0.5f;
        private float width = 25;
        private float height = 18;

        private bool _isSpawning;

        public SpawnHelper(SpawnSystem spawnSystem, BattleSystem battleSystem) {
            _spawnSystem = spawnSystem;
            _battleSystem = battleSystem;
            _enemyCollectionAggregator = _battleSystem.CollectionAggregator;
        }

        public void OnUpdate() {
            if (_battleSystem.EnemyEntities.Count >= MaxUnits) return;
            if (_isSpawning) return;
            Main.Routine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine() {
            _isSpawning = true;
            yield return new WaitForSeconds(SpawnDelay);
            var spawnPosition = GetRandomSpawnPosition();
            var enemyData = _enemyCollectionAggregator.GetRandomEnemy();
            _spawnSystem.SpawnEnemy(enemyData, spawnPosition, Quaternion.identity, OnEnemySpawned);
            _isSpawning = false;
        }

        private void OnEnemySpawned(EnemyEntity enemyEntity) {
        }

        private Vector3 GetRandomSpawnPosition() {
            var randomAngle = Random.Range(0, 360);
            var angleRad = randomAngle * Mathf.Deg2Rad;
            var sin = Mathf.Sin(angleRad);
            var cos = Mathf.Cos(angleRad);
            var x = Mathf.Sign(cos) * Mathf.Pow(Mathf.Abs(cos), 0.55f);
            var z = Mathf.Sign(sin) * Mathf.Pow(Mathf.Abs(sin), 0.55f);
            return new Vector3(x * width, 0, z * height);
        }

        public void Dispose() {
            _spawnSystem = null;
            _battleSystem = null;
            _enemyCollectionAggregator = null;
        }
    }

}