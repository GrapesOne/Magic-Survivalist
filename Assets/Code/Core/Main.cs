using System.Collections;
using Code.Core.Managers;
using Code.Core.Utilities;
using Code.Input;
using Code.Input.Data;
using Code.UI;
using UnityEngine;

namespace Code.Core {

    public class Main : MonoBehaviour {
        public static Main Instance;

        [SerializeField] private CoroutineRunner coroutineRunnerPrefab;
        [SerializeField] private UIManager MainUI;
        [SerializeField] private InputHandler inputHandlerPrefab;
        public InputHandler InputHandler { get; private set; }
        public GameStateManager GameStateManager { get; private set; }
        public UIScreens UIScreens { get; private set; }
        public ICoroutineRunner CoroutineRunner { get; private set; }

        private void Awake() {
            Debug.Log($"Awake");
            Instance = this;
            DontDestroyOnLoad(this);
            Application.targetFrameRate = 60;
        }

        private void Start() {
            Debug.Log($"Start");
            CreateObjects();
            InitObjects();
            TimeUtility.StartGameTime();
            GameStateManager.ChangeState(GameStateType.Gameplay);
        }

        private void Update() {
            GameStateManager.OnUpdate();
            InputHandler.OnUpdate();
        }

        private void FixedUpdate() {
            GameStateManager.OnFixedUpdate();
        }

        private void LateUpdate() {
            GameStateManager.OnLateUpdate();
        }

        private void CreateObjects() {
            CoroutineRunner = Instantiate(coroutineRunnerPrefab);
            InputHandler = Instantiate(inputHandlerPrefab);

            UIScreens = new UIScreens();
            GameStateManager = new GameStateManager(CoroutineRunner);
        }

        public void InitObjects() {
            InputHandler.Init(InputType.Keyboard);
        }

        public static Coroutine Routine(IEnumerator coroutine) => Instance.CoroutineRunner.StartCoroutine(coroutine);
    }

}