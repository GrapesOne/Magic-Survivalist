using Code.Combat.Data;
using Code.Combat.Systems;
using Code.Core.Data;
using Code.Core.Managers;
using Code.Core.Utilities;
using Code.UI;

namespace Code.Core.StateManagement {

    public class GameplayState : BaseGameState {
        public BattleSystem BattleSystem { get; set; }
        private GameplayScreen _screen;

        public GameplayState(GameStateManager stateManager) : base(stateManager) {
        }

        public override SceneType SceneName => SceneType.Gameplay;
        public override GameStateType StateType => GameStateType.Gameplay;
        public override string StateName => "Game";
        public override BaseGameStateController Controller { get; protected set; }
        public GameplayController GameplayController => Controller as GameplayController;

        public override void OnEnter(object data) {
        }

        public override void OnAction(GameAction action, object data = null) {
            base.OnAction(action, data);

            switch (action) {
                case GameAction.Pause:
                    _screen.ShowPauseMenu();
                    TimeUtility.StopGameTime();
                    break;
            }
        }
        public override void SetController(BaseGameStateController controller) {
            _screen = UIScreens.Get<GameplayScreen>();
            _screen.Show();
            _screen.TryInitialize();

            Controller = controller as GameplayController;
            Controller.Init(null);
            CreateSystems();

            EventBus.OnProjectileSelected += OnProjectileSelected;
            
            ImperativeInstructions.RunGame();
        }
        

        private void CreateSystems() {
            BattleSystem = new BattleSystem(GameplayController.ParentStructs, _screen.UpdateEnemiesCount);
        }
        
        
        public override void OnUpdate() {
            base.OnUpdate();
            BattleSystem?.OnUpdate();
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();
            BattleSystem?.OnFixedUpdate();
        }

        public override void OnLateUpdate() {
            base.OnLateUpdate();
            BattleSystem?.OnLateUpdate();
        }

        public override void SetControllerData(object data) {
        }
        
        
        private void OnProjectileSelected(BaseUnitCombatData obj) {
            var material = BattleSystem.CollectionAggregator.GetAdditionalMaterial(obj.colorVisualType);
            _screen.OnProjectileSelected(material);
        }
        
        
        
        public override void OnExit(object data) {
            BattleSystem.DestroySubSystems();
            BattleSystem = null;

            EventBus.OnProjectileSelected -= OnProjectileSelected;
            _screen.Hide();
        }
    }

}