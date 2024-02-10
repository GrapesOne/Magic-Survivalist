using System;
using System.Collections.Generic;
using Code.Core.Managers;
using UnityEngine;
using Object = UnityEngine.Object;

//Purpose: To manage the UI screens in the game
namespace Code.UI {
    public class UIScreens {
        private static readonly Dictionary<Type, UIPanel> Screens = new();

        public UIScreens() {
            CreateScreens();
        }

        private void CreateScreens() {
            var parent = UIManager.Instance.ScreenCanvas;
            var gameScreen = Object.Instantiate(Resources.Load<GameplayScreen>(GameplayScreen.Path), parent.transform);

            Screens.Add(typeof(GameplayScreen), gameScreen);
        }

        public static void Add<T>(T uiScreen) where T : UIPanel {
            if (!Screens.ContainsKey(typeof(T))) Screens.Add(typeof(T), uiScreen);
            else Debug.LogError($"Key {typeof(T)} already exist.");
        }

        public static T Get<T>() where T : UIPanel {
            if (Screens.ContainsKey(typeof(T))) return Screens[typeof(T)] as T;
            Debug.LogError($"Key {typeof(T)} doesn't exist.");
            return null;

        }
    }
}