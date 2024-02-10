using UnityEngine;

namespace Code.Combat.Units.Controllers {

    public class EnemyUnitController : UnitController {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;

        public override void Init(Material baseMaterial, Material additionalMaterial) {
            base.Init(baseMaterial, additionalMaterial);
            meshRenderer.material = baseMaterial;
        }

        public void MoveToPosition(Vector3 vector3) {
            _transform.position = vector3;
        }

        public void LookAt(Vector3 pos) {
            _transform.LookAt(pos);
        }
    }

}