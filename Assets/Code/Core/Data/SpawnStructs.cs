using System;
using Code.Combat.Data;
using UnityEngine;

namespace Code.Core.Data {

    [Serializable]
    public struct ParentStruct {
        public UnitType parentType;
        public Transform parent;
    }

}