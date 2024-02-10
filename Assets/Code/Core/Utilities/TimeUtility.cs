using UnityEngine;

namespace Code.Core.Utilities {

    public static class TimeUtility {
        private static float _timeScale;

        public static void StopGameTime() {
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public static void StartGameTime() {
            Time.timeScale = 1;
        }

        public static void ResumeGameTime() {
            Time.timeScale = _timeScale > 0 ? _timeScale : 1;
        }
    }

}