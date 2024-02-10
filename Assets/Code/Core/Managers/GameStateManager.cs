using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core.Data;
using Code.Core.StateManagement;
using Code.Core.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

namespace Code.Core.Managers {

    public enum GameStateType {
        Gameplay,
    }
    
    public enum SceneType {
        None = -1,
        Startup = 0,
        Gameplay = 1,
    }

    public class GameStateManager {
        
        private readonly Dictionary<GameStateType, BaseGameState> _states = new();
        private readonly Dictionary<SceneType, int> _scenes = new();
        
        public Scene CurrentScene { get; private set; }
        
        public BaseGameState PreviousGameState { get; private set; }
        public BaseGameState CurrentGameState { get; private set; }
        
        private ICoroutineRunner _coroutineRunner;

        public static Action<BaseGameState> GameStateChanged;
        
        public AsyncOperation SceneLoadingOperation { get; private set; }

        public GameStateManager(ICoroutineRunner coroutineRunner) {

            _coroutineRunner = coroutineRunner;
            MapScenes();
            
            SetupStates();
        }
        
        private void MapScenes() {
            _scenes.Add(SceneType.Gameplay, (int)SceneType.Gameplay);
        }

        private void SetupStates() {
            _states.Add(GameStateType.Gameplay, new GameplayState(this));
        }
        
        public bool ChangeState(GameStateType stateType, object data = null) {
            return ChangeState(_states[stateType], data);
        }

        public bool ChangeState(BaseGameState state, object data = null) {
            if (state == null) { // close current state
                if (CurrentGameState == null) return true; // Clear current state
                CurrentGameState.OnExit(data);
                PreviousGameState = CurrentGameState;
                CurrentGameState = null;
                return true;
            }

            if (CurrentGameState == state) return false;
            if (CurrentGameState != null) {
                PreviousGameState = CurrentGameState;
                CurrentGameState.OnExit(data);
            }
            
            CurrentGameState = state;
                
            if (CurrentGameState != null && CurrentGameState.SceneName.ToString() != CurrentScene.name) {
                LoadScene(CurrentGameState.SceneName, () => OnStateChanged(state, data));
            } else {
                OnStateChanged(state, data);
            }

            return true;

        }

        private void OnStateChanged(BaseGameState state, object data) {
            CurrentGameState.OnEnter(data);
            GameStateChanged?.Invoke(state);
        }

        public void OnUpdate() {
            CurrentGameState?.OnUpdate();
        }
        
        public void OnFixedUpdate() {
            CurrentGameState?.OnFixedUpdate();
        }
        public void OnLateUpdate() {
            CurrentGameState?.OnLateUpdate();
        }

        public void HandleGameAction(GameAction action, object data = null) {
            CurrentGameState.OnAction(action, data);
        }

        public void SetControllerToCurrentState(BaseGameStateController controller) {
            CurrentGameState.SetController(controller);
        }

        public void LoadScene(SceneType type, Action onLoadFinished) {
            _coroutineRunner.StartCoroutine(AsyncLoad(type, onLoadFinished));
        }

        private IEnumerator AsyncLoad(SceneType type, Action onLoadCallback) {
            var sceneIndex = _scenes[type];

            SceneLoadingOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
            SceneLoadingOperation.allowSceneActivation = false;
            var _progress = 0f;
            
            while (!SceneLoadingOperation.isDone) {
                if (!Mathf.Approximately(SceneLoadingOperation.progress, _progress)) {
                    _progress = SceneLoadingOperation.progress;
                }

                if (SceneLoadingOperation.progress >= 0.9f) {
                    SceneLoadingOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            _progress = 1f;

            CurrentScene = SceneManager.GetSceneByBuildIndex(sceneIndex);
            SceneManager.SetActiveScene(CurrentScene);
            
            onLoadCallback?.Invoke();
        }
        
        
    }
}