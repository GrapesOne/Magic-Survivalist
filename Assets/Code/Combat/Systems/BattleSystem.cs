using System;
using System.Collections.Generic;
using Code.Combat.Units.Controllers;
using Code.Combat.Units.Entities;
using Code.Core;
using Code.Core.Data;
using Code.Core.Utilities;

//The system controls all systems that are used during combat
namespace Code.Combat.Systems {

    public class BattleSystem {
        public CollectionAggregator CollectionAggregator;
        public SpawnSystem SpawnSystem;
        public MovementSystem MovementSystem;
        public BehaviourSystem BehaviourSystem;
        public AttackSystem AttackSystem;
        public CollisionSystem CollisionSystem;
        public BattleAreaService BattleAreaService;
        public SpawnHelper SpawnHelper;


        public List<UnitController> OtherUnits = new();
        public List<EnemyUnitController> EnemyUnits = new();
        public List<ProjectileController> ProjectileUnits = new();
        public List<EnemyEntity> EnemyEntities = new();
        public List<ProjectileEntity> ProjectileEntities = new();
        public PlayerUnitController PlayerUnit;
        public PlayerEntity PlayerEntity;

        //ducttape
        public static PlayerEntity EnemyTarget;

        private Action<int> _onEnemiesCountChanged;
        public BattleSystem(List<ParentStruct> parentStructs, Action<int> onEnemiesCountChanged) {
            TimeUtility.ResumeGameTime();
            _onEnemiesCountChanged = onEnemiesCountChanged;
            SetupSubSystems(parentStructs);
            SubscribeToEvents();
        }

        private void SetupSubSystems(List<ParentStruct> parentStructs) {
            CollectionAggregator = new CollectionAggregator();
            MovementSystem = new MovementSystem(this);
            BehaviourSystem = new BehaviourSystem(this);
            CollisionSystem = new CollisionSystem(this);
            AttackSystem = new AttackSystem(this, CollisionSystem);
            BattleAreaService = new BattleAreaService(this);
            SpawnSystem = new SpawnSystem(this, parentStructs);
            SpawnHelper = new SpawnHelper(SpawnSystem, this);
        }

        private void SubscribeToEvents() {
            EventBus.OnUnitControllerRegistered += AddNewUnitController;
            EventBus.OnUnitEntityRegistered += AddNewUnitEntity;
            EventBus.OnUnitControllerDisposed += RemoveUnitController;
            EventBus.OnUnitEntityDisposed += RemoveUnitEntity;
        }

        public void Dispose() {
            MovementSystem.Dispose();
            BehaviourSystem.Dispose();
            CollisionSystem.Dispose();
            AttackSystem.Dispose();
            BattleAreaService.Dispose();
            SpawnSystem.Dispose();
            SpawnHelper.Dispose();
            EventBus.OnUnitControllerRegistered -= AddNewUnitController;
            EventBus.OnUnitEntityRegistered -= AddNewUnitEntity;
            EventBus.OnUnitControllerDisposed -= RemoveUnitController;
            EventBus.OnUnitEntityDisposed -= RemoveUnitEntity;
        }

        private void AddNewUnitController(UnitController unit) {
            switch (unit) {
                case PlayerUnitController playerUnitController:
                    PlayerUnit = playerUnitController;
                    PlayerEntity?.SetController(PlayerUnit);
                    break;
                case EnemyUnitController enemyUnitController:
                    if (!EnemyUnits.Contains(enemyUnitController))
                        EnemyUnits.Add(enemyUnitController);
                    break;
                case ProjectileController projectileController:
                    if (!ProjectileUnits.Contains(projectileController))
                        ProjectileUnits.Add(projectileController);
                    break;
                default:
                    if (!OtherUnits.Contains(unit))
                        OtherUnits.Add(unit);
                    break;
            }
        }

        private void AddNewUnitEntity(UnitEntity unit) {
            switch (unit) {
                case PlayerEntity playerEntity:
                    PlayerEntity = playerEntity;
                    if (PlayerUnit != null) {
                        PlayerEntity.SetController(PlayerUnit);
                        PlayerEntity.Init();
                        PlayerEntity.SetProjectileAggregator(CollectionAggregator);
                    }

                    EnemyTarget = PlayerEntity;
                    break;
                case ProjectileEntity projectileEntity:
                    ProjectileEntities.Add(projectileEntity);
                    break;
                case EnemyEntity enemyEntity:
                    EnemyEntities.Add(enemyEntity);
                    _onEnemiesCountChanged?.Invoke(EnemyEntities.Count);
                    break;
            }
        }

        private void RemoveUnitEntity(UnitEntity unit) {
            switch (unit) {
                case PlayerEntity playerEntity:
                    PlayerEntity = null;
                    EnemyTarget = null;
                    break;
                case EnemyEntity enemyEntity:
                    if (EnemyEntities.Contains(enemyEntity)) {
                        EnemyEntities.Remove(enemyEntity);
                        _onEnemiesCountChanged?.Invoke(EnemyEntities.Count);
                    }

                    break;
                case ProjectileEntity projectileEntity:
                    if (ProjectileEntities.Contains(projectileEntity))
                        ProjectileEntities.Remove(projectileEntity);
                    break;
            }
        }

        private void RemoveUnitController(UnitController unit) {
            switch (unit) {
                case PlayerUnitController playerUnitController:
                    PlayerUnit = null;
                    break;
                case EnemyUnitController enemyUnitController:
                    if (EnemyUnits.Contains(enemyUnitController))
                        EnemyUnits.Remove(enemyUnitController);
                    break;
                case ProjectileController projectileController:
                    if (ProjectileUnits.Contains(projectileController))
                        ProjectileUnits.Remove(projectileController);
                    break;
            }
        }

        public void OnFixedUpdate() {
        }

        public void OnUpdate() {
            BehaviourSystem.OnUpdate();
            MovementSystem.OnUpdate();
            SpawnHelper.OnUpdate();
        }

        public void OnLateUpdate() {
            BattleAreaService.OnUpdate();
        }
    }

}