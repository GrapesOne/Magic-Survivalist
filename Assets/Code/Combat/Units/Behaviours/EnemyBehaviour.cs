using Code.Combat.Systems;
using Code.Combat.Units.Controllers;
using Code.Combat.Units.Entities;
using Code.Core;
using UnityEngine;

namespace Code.Combat.Units.Behaviours {

    public class EnemyBehaviour : UnitBehaviour {
        private EnemyUnitController _enemyUnitController;
        private EnemyEntity _enemyEntity;
        public IMovement Movement { get; private set; }

        private bool _canAttack;
        private UnitEntity _target;

        private float previousAttackTime;

        public override void Init(UnitEntity unitEntity, UnitController unitController) {
            _target = BattleSystem.EnemyTarget;
            if (_target == null) return;
            
            base.Init(unitEntity, unitController);
            _enemyUnitController = (EnemyUnitController)unitController;
            _enemyEntity = (EnemyEntity)unitEntity;
            var targetMovement = new TargetMovement();
            targetMovement.SetPositionGetter(_enemyUnitController.GetPosition);
            targetMovement.SetStopDistance(_enemyEntity.Data.attackRange);
            Movement = targetMovement;
            Movement.SetSpeed(_enemyEntity.Data.moveSpeed);
            targetMovement.SetTarget(BattleSystem.EnemyTarget.Controller.transform);
            targetMovement.OnMove += OnMove;
            EventBus.RegisterMovement?.Invoke(Movement);
            previousAttackTime = Time.time;

            EventBus.OnPlayerDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath() {
            _target = null;
            EventBus.OnPlayerDeath -= OnPlayerDeath;
        }

        private void OnMove(Vector3 position) {
            if (_canAttack) {
                return;
            }

            _enemyUnitController.MoveToPosition(position);
        }

        public override void Dispose() {
            base.Dispose();
            EventBus.UnregisterMovement?.Invoke(Movement);
            _enemyUnitController = null;
            _enemyEntity = null;
            _target = null;
            Movement = null;
        }

        public override void OnUpdate() {
            if (Movement is TargetMovement targetMovement) {
                if (_target == null) return;
                _enemyUnitController.LookAt(_target.GetPosition());
                if (targetMovement.IsAtTarget()) {
                    _canAttack = true;
                    Attack();
                }
                else {
                    _canAttack = false;
                }
            }
        }

        private void Attack() {
            if (!CooldownExpired()) return;
            EventBus.OnDirectAttackUnit?.Invoke(_target, _enemyEntity.AttackState);
            previousAttackTime = Time.time;
        }

        private bool CooldownExpired() {
            return Time.time - previousAttackTime > _enemyEntity.Data.attackSpeed;
        }
    }

}