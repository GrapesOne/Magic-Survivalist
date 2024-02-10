using UnityEngine;

namespace Code.Core.Utilities {
    [RequireComponent(typeof(DontDestroyOnLoad))]
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner {
        private void OnDestroy() {
            StopAllCoroutines();
        }
    }
}