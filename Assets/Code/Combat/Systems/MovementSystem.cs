using System.Collections.Generic;
using Code.Combat.Units;
using Code.Core;

namespace Code.Combat.Systems {

    public class MovementSystem {
        private BattleSystem _battleSystem;
        private List<IMovement> _movements;

        public MovementSystem(BattleSystem battleSystem) {
            _battleSystem = battleSystem;
            _movements = new List<IMovement>();
            EventBus.RegisterMovement += RegisterMovement;
            EventBus.UnregisterMovement += UnregisterMovement;
        }

        private void UnregisterMovement(IMovement obj) {
            if (!_movements.Contains(obj)) return;
            _movements.Remove(obj);
        }

        private void RegisterMovement(IMovement movement) {
            if (_movements.Contains(movement)) return;
            _movements.Add(movement);
        }

        public void OnUpdate() {
            for (var i = 0; i < _movements.Count; i++) {
                _movements[i]?.Move();
            }
        }

        public void Dispose() {
            _movements.Clear();
            EventBus.RegisterMovement -= RegisterMovement;
            EventBus.UnregisterMovement -= UnregisterMovement;
        }
    }

}