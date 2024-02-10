using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "EnemyCollection", menuName = "Combat/EnemyCollection", order = 1)]
    public class EnemyCollection : ScriptableObject, IDataCollection {
        public const string Path = "ScriptableObjects/Collections/EnemyCollection";

        [field: SerializeField, Expandable] private List<EnemyData> enemies;
        private EnemyData _lastEnemy;

        public EnemyData GetEnemy(int id) => GetUnitData(id) as EnemyData;
        public EnemyData GetCurrentEnemyOrFirst() => GetCurrentDataOrFirst() as EnemyData;
        public EnemyData GetRandomEnemy() => GetRandomData() as EnemyData;

        public BaseUnitCombatData GetUnitData(int id) {
            foreach (var enemy in enemies) {
                if (enemy.id != id) continue;
                _lastEnemy = enemy;
                return enemy;
            }

            throw new System.Exception($"Enemy with id {id} not found");
        }

        public BaseUnitCombatData GetCurrentDataOrFirst() {
            if (_lastEnemy == null) _lastEnemy = enemies[0];
            return _lastEnemy;
        }

        public BaseUnitCombatData GetRandomData() {
            return enemies[Random.Range(0, enemies.Count)];
        }
    }

}