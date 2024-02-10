using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "EnemyData", menuName = "Combat/Enemy Data")]
    public class EnemyData : BaseUnitCombatData {
        public int maxHealth;
        public int shield;
        public float moveSpeed;
        public float attackRange;
        public int attackSpeed;
        public int attackDamage;
    }

}