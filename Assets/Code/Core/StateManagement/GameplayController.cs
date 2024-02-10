using System.Collections.Generic;
using Code.Core.Data;
using UnityEngine;

namespace Code.Core.StateManagement {

    public class GameplayController : GameStateController<GameplayController>
    {
        [field: SerializeField] public List<ParentStruct> ParentStructs;   
        public override void Init(object data) {
       
        }

        private void OnEnable() {
            Main.Instance.GameStateManager.SetControllerToCurrentState(this);
        }
    }

}
