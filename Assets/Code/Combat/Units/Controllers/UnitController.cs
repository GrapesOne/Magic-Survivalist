using Code.Core;
using UnityEngine;

namespace Code.Combat.Units.Controllers {

    public abstract class UnitController : MonoBehaviour {
      
        protected Transform _transform;
        public Vector3 GetPosition() => _transform.position;
        public Vector3 SetPosition(Vector3 position) => _transform.position = position;
        public Vector3 GetForward() => _transform.forward;
        public Quaternion GetRotation() => _transform.rotation;
        
        [field: SerializeField] public Collider Collider { get; private set; }

        public virtual void Init(Material baseMaterial, Material additionalMaterial) {
            _transform = transform;
            RegisterUnitController();
        }

        private void RegisterUnitController() {
            EventBus.OnUnitControllerRegistered?.Invoke(this);
        }

        public void Dispose() {
            EventBus.OnUnitControllerDisposed?.Invoke(this);
            Destroy(gameObject);
        }

      
    }

}