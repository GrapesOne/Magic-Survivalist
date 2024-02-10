// Purpose: GameplayScreen class used to control the gameplay screen UI.

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI {
    public class GameplayScreen : UIPanel {
        public static string ScreenName => "GameplayScreen";
        public static string Path => "UI/Screens/GameplayScreen";

        [SerializeField] private TMP_Text enemyCounterText;
        [SerializeField] private Image currentSpellImage;
        
        protected override void OnInitialize() {
        }
        
        public void ShowPauseMenu() {
        }

        public void OnProjectileSelected(Material material) {
            currentSpellImage.color = material.color;
        }

        public void UpdateEnemiesCount(int count) {
            enemyCounterText.text = count.ToString();
            
        }
    }

}