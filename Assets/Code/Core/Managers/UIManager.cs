using UnityEngine;

namespace Code.Core.Managers {

    public class UIManager : MonoBehaviour {
        public static UIManager Instance;

        [field: SerializeField] public Canvas ScreenCanvas { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }

            DontDestroyOnLoad(this);
        }
    }

}