using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "BattleAreaData", menuName = "Combat/BattleAreaData", order = 0)]
    public class BattleAreaData : ScriptableObject
    {
        public const string Path = "ScriptableObjects/BattleAreaData";
        public float width;
        public float height;
        public float centerX;
        public float centerZ;
    }

}
