using System;
using Code.Combat.Data;
using Code.Combat.Units.Controllers;
using UnityEngine;

namespace Code.Combat {

    public class CollectionAggregator : IProjectileAggregator, IEnemyCollectionAggregator {
        private VisualCollection VisualCollection;
        private EnemyCollection EnemyCollection;
        private ProjectileCollection ProjectileCollection;
        private PlayerCollection PlayerCollection;

        public CollectionAggregator() {
            VisualCollection = Resources.Load<VisualCollection>(VisualCollection.Path);
            EnemyCollection = Resources.Load<EnemyCollection>(EnemyCollection.Path);
            ProjectileCollection = Resources.Load<ProjectileCollection>(ProjectileCollection.Path);
            PlayerCollection = Resources.Load<PlayerCollection>(PlayerCollection.Path);
            VisualCollection.Init();
        }

        public BaseUnitCombatData GetUnitData(int id, UnitType unitType) => GetDataCollection(unitType).GetUnitData(id);
        public UnitController GetVisual(VisualType visualType) => VisualCollection.GetVisual(visualType);

        public Material GetBaseMaterial(ColorVisualType colorVisualType) =>
            VisualCollection.GetBaseMaterial(colorVisualType);

        public Material GetAdditionalMaterial(ColorVisualType colorVisualType) =>
            VisualCollection.GetAdditionalMaterial(colorVisualType);

        
        public ProjectileData GetProjectileData(int id) => ProjectileCollection.GetProjectile(id);
        public ProjectileData GetCurrentProjectileOrFirst() => ProjectileCollection.GetCurrentProjectileOrFirst();
        public ProjectileData GetRandomProjectile() => ProjectileCollection.GetRandomProjectile();
        public ProjectileData GetNextProjectile() => ProjectileCollection.GetNextProjectile();
        public ProjectileData GetPreviousProjectile() => ProjectileCollection.GetPreviousProjectile();
        
        
        public EnemyData GetEnemyData(int id) => EnemyCollection.GetEnemy(id);
        public EnemyData GetCurrentEnemyOrFirst() => EnemyCollection.GetCurrentEnemyOrFirst();
        public EnemyData GetRandomEnemy() => EnemyCollection.GetRandomEnemy();

        private IDataCollection GetDataCollection(UnitType unitType) {
            return unitType switch {
                UnitType.player => PlayerCollection,
                UnitType.enemy => EnemyCollection,
                UnitType.projectile => ProjectileCollection,
                _ => throw new ArgumentOutOfRangeException(nameof(unitType), unitType, null)
            };
        }

      
    }

}