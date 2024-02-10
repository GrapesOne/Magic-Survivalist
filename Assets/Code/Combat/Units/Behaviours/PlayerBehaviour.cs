using Code.Combat.Units.Controllers;
using Code.Combat.Units.Entities;
using Code.Input;

namespace Code.Combat.Units.Behaviours {

    public class PlayerBehaviour : UnitBehaviour {
        private PlayerUnitController _playerUnitController;
        private PlayerEntity _playerEntity;
        private float _forwardSpeed;
        private float _backwardSpeed;
        
        public override void Init(UnitEntity unitEntity, UnitController unitController) {
            base.Init(unitEntity, unitController);
            _playerUnitController = (PlayerUnitController) unitController;
            _playerEntity = (PlayerEntity) unitEntity;
            InputBus.OnMoveUpHold += MoveForward;
            InputBus.OnMoveDownHold += MoveBackward;
            InputBus.OnMoveLeftHold += RotateLeft;
            InputBus.OnMoveRightHold += RotateRight;
            _forwardSpeed = _playerEntity.Data.moveSpeed;
            _backwardSpeed = _playerEntity.Data.moveSpeed * -1;
        }
        public override void Dispose() {
            base.Dispose();
            _playerUnitController = null;
            _playerEntity = null;
            InputBus.OnMoveUpHold -= MoveForward;
            InputBus.OnMoveDownHold -= MoveBackward;
            InputBus.OnMoveLeftHold -= RotateLeft;
            InputBus.OnMoveRightHold -= RotateRight;
        }
    
        public override void OnUpdate() {
        
        }
        private void MoveForward() {
            _playerUnitController.Move(_forwardSpeed);
        }
        private void MoveBackward() {
            _playerUnitController.Move(_backwardSpeed);
        }
        private void RotateLeft() {
            _playerUnitController.Rotate(_backwardSpeed);
        }
        private void RotateRight() {
            _playerUnitController.Rotate(_forwardSpeed);
        }
    }

}
