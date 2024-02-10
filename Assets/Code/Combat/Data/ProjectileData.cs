using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Combat/Projectile Data")]
    public class ProjectileData : BaseUnitCombatData {
        public float moveSpeed;
        public int attackDamage;
    }

}