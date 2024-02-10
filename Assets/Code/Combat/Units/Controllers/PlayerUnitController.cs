using UnityEngine;

namespace Code.Combat.Units.Controllers {

    public class PlayerUnitController : UnitController {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float rotationCoefficient;
        
        public override void Init(Material baseMaterial, Material additionalMaterial) {
            base.Init(baseMaterial, additionalMaterial);
            meshRenderer.material = baseMaterial;
        }
        
        public void Move(float speed) {
            _transform.position += _transform.forward * (speed * Time.deltaTime);
        }
        
        public void Rotate(float rotationSpeed) {
            _transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * rotationCoefficient);
        }
    }

}