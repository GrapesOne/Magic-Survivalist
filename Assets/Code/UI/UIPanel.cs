using System;

using UnityEngine;

namespace Code.UI {

    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour {
        protected CanvasGroup CanvasGroup { get; private set; }
        private bool _isInitialized;

        protected void Awake() {
            gameObject.SetActive(false);

            CanvasGroup = GetComponent<CanvasGroup>();
            CanvasGroup.alpha = 0f;
        }

        public void TryInitialize() {
            if (_isInitialized)
                return;

            OnInitialize();
            _isInitialized = true;
        }

        protected abstract void OnInitialize();

        public void Show(Action callback = null) {
            if (gameObject.activeSelf) return;

            CanvasGroup.alpha = 1f;
            gameObject.SetActive(true);
            callback?.Invoke();
        }

        public void Hide() {
            if (!gameObject.activeSelf) return;

            CanvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
    }

}