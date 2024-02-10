using UnityEngine;

namespace Code.Core.StateManagement {

    public abstract class GameStateController<T> : BaseGameStateController where T : GameStateController<T> {
        public static T Instance { get; private set; }

        protected virtual void Awake() {
            Instance = this as T;
        }

        protected virtual void OnDestroy() {
            Instance = null;
        }
    }

    public abstract class BaseGameStateController : MonoBehaviour {
        public abstract void Init(object data);
    }

}