using Code.Combat.Units.Entities;
using Code.Core;

namespace Code.Combat.Systems {

    public class AttackSystem
    {
        private BattleSystem _battleSystem;
        private CollisionSystem _collisionSystem;

        public AttackSystem(BattleSystem battleSystem, CollisionSystem collisionSystem) {
            _battleSystem = battleSystem;
            _collisionSystem = collisionSystem;

            EventBus.OnDirectAttackUnit += HandleDirectAttack;
        }

        private void HandleDirectAttack(UnitEntity unit, AttackState attackState) {
            attackState.Context.SetReceiver(unit);
            unit.HandleCollision(attackState);
        }

        public void Dispose() {
            _battleSystem = null;
            _collisionSystem = null;
            EventBus.OnDirectAttackUnit -= HandleDirectAttack;
        }
    }

}
