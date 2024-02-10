using System.Collections.Generic;
using Code.Combat.Units.Behaviours;
using Code.Core;

namespace Code.Combat.Systems {

    public class BehaviourSystem {
        private BattleSystem _battleSystem;
        private List<UnitBehaviour> _behaviours;

        public BehaviourSystem(BattleSystem battleSystem) {
            _battleSystem = battleSystem;
            _behaviours = new List<UnitBehaviour>();
            EventBus.RegisterBehaviour += RegisterBehaviour;
            EventBus.UnregisterBehaviour += UnregisterBehaviour;
        }

        public void UnregisterBehaviour(UnitBehaviour unitBehaviour) {
            _behaviours.Remove(unitBehaviour);
        }

        private void RegisterBehaviour(UnitBehaviour unitBehaviour) {
            _behaviours.Add(unitBehaviour);
        }

        public void OnUpdate() {
            foreach (var behaviour in _behaviours) {
                behaviour.OnUpdate();
            }
        }

        public void Dispose() {
            _behaviours.Clear();
            EventBus.RegisterBehaviour -= RegisterBehaviour;
            EventBus.UnregisterBehaviour -= UnregisterBehaviour;
        }
    }

}