using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "ProjectileCollection", menuName = "Combat/ProjectileCollection", order = 1)]
    public class ProjectileCollection : ScriptableObject, IDataCollection{
        public const string Path = "ScriptableObjects/Collections/ProjectileCollection";

        [field: SerializeField, Expandable] private List<ProjectileData> projectiles;
        private ProjectileData _lastProjectile;
        private int _lastProjectileIndex = 0;

        public ProjectileData GetProjectile(int id) => GetUnitData(id) as ProjectileData;
        public ProjectileData GetCurrentProjectileOrFirst() => GetCurrentDataOrFirst() as ProjectileData;
        public ProjectileData GetRandomProjectile() => GetRandomData() as ProjectileData;
        private ProjectileData GetProjectileWithShift(int shift) {
            _lastProjectileIndex += shift;
            if (_lastProjectileIndex < 0) _lastProjectileIndex = projectiles.Count - 1;
            else if (_lastProjectileIndex >= projectiles.Count) _lastProjectileIndex = 0;
            _lastProjectile = projectiles[_lastProjectileIndex];
            return _lastProjectile;
        }

        public ProjectileData GetNextProjectile() => GetProjectileWithShift(1);
        public ProjectileData GetPreviousProjectile() => GetProjectileWithShift(-1);

        public BaseUnitCombatData GetUnitData(int id) {
            for (var i = 0; i < projectiles.Count; i++) {
                if (projectiles[i].id != id) continue;
                _lastProjectileIndex = i;
                _lastProjectile = projectiles[i];
                return _lastProjectile;
            }

            throw new System.Exception($"Projectile with id {id} not found");
        }

        public BaseUnitCombatData GetCurrentDataOrFirst() {
            if (_lastProjectile == null) _lastProjectile = projectiles[0];
            return _lastProjectile;
        }

        public BaseUnitCombatData GetRandomData() {
          
            _lastProjectileIndex = Random.Range(0, projectiles.Count);
            _lastProjectile = projectiles[_lastProjectileIndex];
            return _lastProjectile;
        }
    }

}