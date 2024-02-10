using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "PlayerData", menuName = "Combat/Player Data")]
    public class PlayerData : BaseUnitCombatData {
        public int maxHealth;
        public int shield;
        public float moveSpeed;
    }

}