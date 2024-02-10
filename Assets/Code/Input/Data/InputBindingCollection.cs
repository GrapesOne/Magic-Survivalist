using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Input.Data {

    [CreateAssetMenu(fileName = "InputBindingCollection", menuName = "Input/Input Binding Collection")]
    public class InputBindingCollection : ScriptableObject {
        public static string Path = "ScriptableObjects/Other/InputBindingCollection";

        [field: SerializeField, Expandable] private List<InputBindingData> inputBindings;
        private readonly Dictionary<InputType, InputBindingData> _inputBindings = new();

        public InputBindingData GetInputBinding(InputType type) {
            if (_inputBindings.TryGetValue(type, out var inputBinding)) {
                return inputBinding;
            }

            throw new System.Exception($"InputBinding with type {type} not found");
        }

        public void Init() {
            _inputBindings.Clear();
            foreach (var inputBinding in inputBindings) {
                if (_inputBindings.ContainsKey(inputBinding.inputType)) continue;
                _inputBindings.Add(inputBinding.inputType, inputBinding);
            }
        }
    }

}