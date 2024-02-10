using System;
using System.Collections.Generic;
using Code.Combat.Units.Entities;
using Code.Core;
using UnityEngine;

namespace Code.Combat.Systems {

    public class CollisionSystem {
        private BattleSystem _battleSystem;

        private readonly Dictionary<Collider, UnitEntity> _unitColliders = new();

        private static CollisionSystem _instance;

        public CollisionSystem(BattleSystem battleSystem) {
            _battleSystem = battleSystem;
            _instance = this;
            EventBus.OnUnitEntityRegistered += AddUnitCollider;
            EventBus.OnProjectileCollision += HandleProjectileCollision;
        }


        private void AddUnitCollider(UnitEntity entity) {
            entity.OnSetController += controller => {
                if (_unitColliders.ContainsKey(controller.Collider)) {
                    Debug.LogWarning($"Cannot add entity {entity.GetName()}.  Already in the list!");
                    return;
                }

                _unitColliders.Add(controller.Collider, entity);
            };
        }

        public void Dispose() {
            _battleSystem = null;
            _instance = null;
            _unitColliders.Clear();
            EventBus.OnUnitEntityRegistered -= AddUnitCollider;
        }

        private void HandleProjectileCollision(Collider collider, AttackState attackState, Action<bool> callback) {
            if (collider == null || !_unitColliders.ContainsKey(collider)) {
                callback?.Invoke(false);
                return;
            }

            var entity = _unitColliders[collider];
            if (entity.Team == attackState.SourceTeam) {
                callback?.Invoke(false);
                return;
            }

            attackState.Context.SetReceiver(entity);
            entity.HandleCollision(attackState);
            callback?.Invoke(true);
        }
    }

}