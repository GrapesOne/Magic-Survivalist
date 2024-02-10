using System;
using Code.Combat;
using Code.Combat.Data;
using Code.Combat.Units;
using Code.Combat.Units.Behaviours;
using Code.Combat.Units.Controllers;
using Code.Combat.Units.Entities;
using UnityEngine;

namespace Code.Core {

    public static class EventBus {
        public static Action<UnitController> OnUnitControllerRegistered { get; set; }
        public static Action<UnitController> OnUnitControllerDisposed { get; set; }

        public static Action<UnitEntity> OnUnitEntityRegistered { get; set; }
        public static Action<UnitEntity> OnUnitEntityDisposed { get; set; }

        public static Action<IMovement> RegisterMovement { get; set; }
        public static Action<IMovement> UnregisterMovement { get; set; }
        
        public static Action<UnitBehaviour> RegisterBehaviour { get; set; }
        public static Action<UnitBehaviour> UnregisterBehaviour { get; set; }
        
        public static Action<UnitController> RegisterControllerInBattleArea { get; set; }

        public static Action<ProjectileData, Vector3, Quaternion, Action<ProjectileEntity>> SpawnProjectile { get; set;}
        public static Action<ProjectileData> OnProjectileSelected { get; set; }
        public static Action<Collider, AttackState, Action<bool>> OnProjectileCollision { get; set; }
        
        public static Action<UnitEntity, AttackState> OnDirectAttackUnit { get; set; }

        public static Action OnPlayerDeath { get; set; }
    }

}