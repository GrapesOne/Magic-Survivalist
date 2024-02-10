using Code.Core.Data;
using Code.Core.Managers;

namespace Code.Core.StateManagement {

    public abstract class BaseGameState {
        protected GameStateManager StateManager { get; }

        public abstract SceneType SceneName { get; }
        public abstract GameStateType StateType { get; }
        public abstract string StateName { get; }

        public abstract BaseGameStateController Controller { get; protected set; }

        public BaseGameState(GameStateManager stateManager) {
            StateManager = stateManager;
        }

        public virtual void OnAction(GameAction action, object data = null) { }
        public abstract void OnEnter(object data);
        public abstract void OnExit(object data);
        public abstract void SetController(BaseGameStateController controller);
        public abstract void SetControllerData(object data);
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void  OnLateUpdate() { }
    }

}