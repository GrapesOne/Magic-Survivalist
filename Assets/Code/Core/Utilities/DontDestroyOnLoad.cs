using UnityEngine;

namespace Code.Core.Utilities {
    public class DontDestroyOnLoad : MonoBehaviour {
        private void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}