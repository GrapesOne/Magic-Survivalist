using System.Collections.Generic;
using Code.Combat.Data;
using Code.Combat.Units.Controllers;
using Code.Core;
using UnityEngine;

namespace Code.Combat.Systems {

    //Purpose: To keep all units within the battle area
    public class BattleAreaService {
        private BattleSystem _battleSystem;
        private List<UnitController> _unitControllers;
        private BattleAreaData _battleAreaData;

        private Vector3 _battleAreaCenter;
        private Vector3 _unitPosition;
        private Vector3 _leftBottomCorner;
        private Vector3 _rightTopCorner;

        public BattleAreaService(BattleSystem battleSystem) {
            _battleSystem = battleSystem;
            _unitControllers = new List<UnitController>();
            _battleAreaData = Resources.Load<BattleAreaData>(BattleAreaData.Path);
            _battleAreaCenter = new Vector3(_battleAreaData.centerX, 0, _battleAreaData.centerZ);
            _leftBottomCorner = new Vector3(_battleAreaCenter.x - _battleAreaData.width / 2, 0,
                _battleAreaCenter.z - _battleAreaData.height / 2);
            _rightTopCorner = new Vector3(_battleAreaCenter.x + _battleAreaData.width / 2, 0,
                _battleAreaCenter.z + _battleAreaData.height / 2);
            EventBus.RegisterControllerInBattleArea += RegisterController;
            EventBus.OnUnitControllerDisposed += UnregisterController;
        }

        public void Dispose() {
            _battleSystem = null;
            _battleAreaData = null;
            EventBus.RegisterControllerInBattleArea -= RegisterController;
            EventBus.OnUnitControllerDisposed -= UnregisterController;
            _unitControllers.Clear();
        }

        private void RegisterController(UnitController unitController) {
            if (_unitControllers.Contains(unitController)) return;
            _unitControllers.Add(unitController);
        }

        private void UnregisterController(UnitController unitController) {
            if (!_unitControllers.Contains(unitController)) return;
            _unitControllers.Remove(unitController);
        }

        public void OnUpdate() {
            foreach (var unitController in _unitControllers) {
                _unitPosition = unitController.GetPosition();
                if (!IsUnitOutsideBattleArea(_unitPosition)) continue;
                unitController.SetPosition(GetClosestPointInBattleArea(_unitPosition));
            }
        }

        private Vector3 GetClosestPointInBattleArea(Vector3 unitPosition) {
            var x = Mathf.Clamp(unitPosition.x, _leftBottomCorner.x, _rightTopCorner.x);
            var z = Mathf.Clamp(unitPosition.z, _leftBottomCorner.z, _rightTopCorner.z);
            return new Vector3(x, unitPosition.y, z);
        }

        private bool IsUnitOutsideBattleArea(Vector3 unitPosition) {
            return unitPosition.x < _leftBottomCorner.x || unitPosition.x > _rightTopCorner.x ||
                   unitPosition.z < _leftBottomCorner.z || unitPosition.z > _rightTopCorner.z;
        }
    }

}