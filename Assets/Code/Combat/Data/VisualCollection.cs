using System;
using System.Collections.Generic;
using Code.Attributes;
using Code.Combat.Units.Controllers;
using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "VisualCollection", menuName = "Combat/VisualCollection", order = 1)]
    public class VisualCollection : ScriptableObject {
        public const string Path = "ScriptableObjects/Collections/VisualCollection";

        [SerializeField, ArrayElementTitle("type", "prefab")]
        private VisualElement[] visuals;
        
        [SerializeField, ArrayElementTitle("type")]
        private ColorSchemeElement[] colorSchemes;


        private readonly Dictionary<VisualType, UnitController> _visuals = new();
        private readonly Dictionary<ColorVisualType, ColorSchemeElement> _colorSchemes = new();


        public UnitController GetVisual(VisualType type) {
            if (_visuals.TryGetValue(type, out var visual)) {
                return visual;
            }

            throw new Exception($"Visual with type {type} not found");
        }
        
        public Material GetBaseMaterial(ColorVisualType type) {
            if (_colorSchemes.TryGetValue(type, out var colorScheme)) {
                return colorScheme.material;
            }

            throw new Exception($"Color scheme with type {type} not found");
        }
        
        public Material GetAdditionalMaterial(ColorVisualType type) {
            if (_colorSchemes.TryGetValue(type, out var colorScheme)) {
                return colorScheme.additionalMaterial;
            }

            throw new Exception($"Color scheme with type {type} not found");
        }


        public void Init() {
            _visuals.Clear();
            foreach (var visual in visuals) {
                if (_visuals.ContainsKey(visual.type)) continue;
                _visuals.Add(visual.type, visual.prefab);
            }
            
            _colorSchemes.Clear();
            foreach (var colorScheme in colorSchemes) {
                if (_colorSchemes.ContainsKey(colorScheme.type)) continue;
                _colorSchemes.Add(colorScheme.type, colorScheme);
            }
        }

        [Serializable]
        public struct VisualElement {
            public VisualType type;
            public UnitController prefab;
        }
        
        [Serializable]
        public struct ColorSchemeElement {
            public ColorVisualType type;
            public Material material;
            public Material additionalMaterial;
        }
    }

}