using System;
using System.Collections.Generic;
using Code.Combat.Data;
using Code.Combat.Units.Controllers;
using Code.Combat.Units.Entities;
using Code.Core;
using Code.Core.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Combat.Systems {

    public class SpawnSystem {
        private BattleSystem _battleSystem;
        private List<ParentStruct> _parentStructs;
        private CollectionAggregator _collectionAggregator;

        public SpawnSystem(BattleSystem battleSystem, List<ParentStruct> parentStructs) {
            _battleSystem = battleSystem;
            _parentStructs = parentStructs;
            _collectionAggregator = _battleSystem.CollectionAggregator;
            EventBus.SpawnProjectile += SpawnProjectile;
        }

        public UnitController SpawnUnitController(int unitId, UnitType unitType, Vector3 position,
            Quaternion rotation) {
            var unitData = _collectionAggregator.GetUnitData(unitId, unitType);
            var visualPrefab = _collectionAggregator.GetVisual(unitData.visualType);
            var baseColorVisual = _collectionAggregator.GetBaseMaterial(unitData.colorVisualType);
            var additionalColorVisual = _collectionAggregator.GetAdditionalMaterial(unitData.colorVisualType);
            var parent = GetParent(unitType);
            var visual = Object.Instantiate(visualPrefab, position, rotation, parent);
            visual.Init(baseColorVisual, additionalColorVisual);
            return visual;
        }

        public PlayerEntity SpawnPlayerEntity(int unitId) {
            var unitData = _collectionAggregator.GetUnitData(unitId, UnitType.player);
            var unitEntity = new PlayerEntity();
            unitEntity.SetData(unitData as PlayerData);
            EventBus.OnUnitEntityRegistered?.Invoke(unitEntity);
            return unitEntity;
        }

        public ProjectileEntity SpawnProjectileEntity(ProjectileData projectileData) {
            var projectileEntity = new ProjectileEntity();
            projectileEntity.SetData(projectileData);
            EventBus.OnUnitEntityRegistered?.Invoke(projectileEntity);
            return projectileEntity;
        }

        public void SpawnProjectile(ProjectileData projectileData, Vector3 position, Quaternion rotation,
            Action<ProjectileEntity> callback = null) {
            var unitController = SpawnUnitController(projectileData.id, UnitType.projectile, position, rotation);
            var projectileEntity = SpawnProjectileEntity(projectileData);
            unitController.SetPosition(unitController.GetPosition() + Vector3.up * 0.5f);
            projectileEntity.SetController(unitController as ProjectileController);
            projectileEntity.Init();
            callback?.Invoke(projectileEntity);
        }

        private Transform GetParent(UnitType parentType) {
            foreach (var parentStruct in _parentStructs) {
                if (parentStruct.parentType == parentType) {
                    return parentStruct.parent;
                }
            }

            return _parentStructs[0].parent;
        }

        public EnemyEntity SpawnEnemyEntity(EnemyData enemyData) {
            var enemyEntity = new EnemyEntity();
            enemyEntity.SetData(enemyData);
            EventBus.OnUnitEntityRegistered?.Invoke(enemyEntity);
            return enemyEntity;
        }

        public void SpawnEnemy(EnemyData projectileData, Vector3 position, Quaternion rotation,
            Action<EnemyEntity> callback = null) {
            var unitController = SpawnUnitController(projectileData.id, UnitType.enemy, position, rotation);
            var enemyEntity = SpawnEnemyEntity(projectileData);
            enemyEntity.SetController(unitController as EnemyUnitController);
            enemyEntity.Init();
            callback?.Invoke(enemyEntity);
        }

        public void Dispose() {
            EventBus.SpawnProjectile -= SpawnProjectile;
            _battleSystem = null;
            _parentStructs = null;
            _collectionAggregator = null;
        }
    }

}