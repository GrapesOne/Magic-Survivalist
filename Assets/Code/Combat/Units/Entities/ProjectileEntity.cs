using Code.Combat.Data;
using Code.Combat.Units.Controllers;
using Code.Core;
using UnityEngine;

namespace Code.Combat.Units.Entities {

    public class ProjectileEntity : UnitEntity<ProjectileController, ProjectileData> {
        public IMovement Movement { get; private set; }
        private AttackState AttackState { get; set; }
        
        private bool _inCollision;
        
        public override void Init() {
            Team = Team.Player;
            var simpleMovement = new SimpleMovement();
            simpleMovement.SetPositionGetter(Controller.GetPosition);
            Movement = simpleMovement;
            Movement.SetSpeed(Data.moveSpeed);
            simpleMovement.SetDirection(Controller.GetForward());
            Movement.OnMove += OnMove;

            EventBus.RegisterMovement?.Invoke(Movement);
            TrySetInitialized();
        }

        private void TrySetInitialized() {
            if (Controller != null && Data != null && Movement != null && AttackState != null) {
                IsInitialized = true;
            }
        }
        public void SetAttackState(AttackState attackState) {
            AttackState = attackState;
            TrySetInitialized();
        }

        private void OnMove(Vector3 vector3) {
            Controller.MoveToPosition(vector3);
            // ducttape
            CheckKill();
        }

        private void CheckKill() {
            var position = Controller.GetPosition();
            // kill if out of bounds
            if (position.x < -20 || position.x > 20 || position.z < -20 || position.z > 20) {
                Dispose();
            }
        }

        public override void HandleCollision(AttackState attackState) {
            //
        }

        public override void SetController(ProjectileController controller) {
            base.SetController(controller);
            Controller.CollisionCallback += OnCollision;
        }

        private void OnCollision(Collider other) {
            if (IsInitialized == false || _inCollision) return;
            _inCollision = true;
            var state = AttackState.GetCopy();
            EventBus.OnProjectileCollision?.Invoke(other, state, CollisionCallback);
        }

        private void CollisionCallback(bool isHit) {
            if (!isHit) {
                _inCollision = false;
                return;
            }
            Dispose();
        }
        
        public override void Dispose() {
            Controller.CollisionCallback -= OnCollision;
            Object.Destroy(Controller.gameObject);
            EventBus.UnregisterMovement?.Invoke(Movement);
            base.Dispose();
            Movement = null;
            AttackState = null;
        }
    }

}