using System;
using UnityEngine;

namespace Code.Combat.Units.Controllers {

    public class ProjectileController : UnitController
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public event Action<Collider> CollisionCallback;
        public void MoveToPosition(Vector3 vector3) {
            _transform.position = vector3;
        }
        public override void Init(Material baseMaterial, Material additionalMaterial) {
            base.Init(baseMaterial, additionalMaterial);
            meshRenderer.material = additionalMaterial;
        }
        
        private void OnTriggerEnter(Collider other) {
            CollisionCallback?.Invoke(other);
        }
    }

}
