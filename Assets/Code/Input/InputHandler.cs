using System;
using Code.Core.Utilities;
using Code.Input.Data;
using UnityEngine;

namespace Code.Input {

    [RequireComponent(typeof(DontDestroyOnLoad))]
    public class InputHandler : MonoBehaviour {
        private bool _isInitialized;
        private InputType _inputType;
        private InputBindingCollection _inputBindings;
        private InputBindingData _currentInputBinding;

        public void Init(InputType inputType) {
            if (_isInitialized) return;
            _inputBindings = Resources.Load<InputBindingCollection>(InputBindingCollection.Path);
            _inputBindings.Init();
            _inputType = inputType;
            _currentInputBinding = _inputBindings.GetInputBinding(inputType);
            _isInitialized = true;
        }

        public InputType GetCurrentlyUsedInputType() => _inputType;

        public void OnUpdate() {
            InputUpdate(InputEnums.MoveUp,
                InputBus.OnMoveUpPress, InputBus.OnMoveUpRelease, InputBus.OnMoveUpHold);
            InputUpdate(InputEnums.MoveDown,
                InputBus.OnMoveDownPress, InputBus.OnMoveDownRelease, InputBus.OnMoveDownHold);
            InputUpdate(InputEnums.RotateLeft,
                InputBus.OnMoveLeftPress, InputBus.OnMoveLeftRelease, InputBus.OnMoveLeftHold);
            InputUpdate(InputEnums.RotateRight,
                InputBus.OnMoveRightPress, InputBus.OnMoveRightRelease, InputBus.OnMoveRightHold);
            InputUpdate(InputEnums.Attack,
                InputBus.OnAttackPress, InputBus.OnAttackRelease, InputBus.OnAttackHold);
            InputUpdate(InputEnums.NextWeapon,
                InputBus.OnNextWeaponPress, InputBus.OnNextWeaponRelease, InputBus.OnNextWeaponHold);
            InputUpdate(InputEnums.PreviousWeapon,
                InputBus.OnPreviousWeaponPress, InputBus.OnPreviousWeaponRelease, InputBus.OnPreviousWeaponHold);
        }

        private void InputUpdate(InputEnums inputEnum, Action downAction, Action upAction, Action holdAction) {
            var keyCode = GetKeyCode(inputEnum);
            if (UnityEngine.Input.GetKeyDown(keyCode)) downAction?.Invoke();
            if (UnityEngine.Input.GetKeyUp(keyCode)) upAction?.Invoke();
            if (UnityEngine.Input.GetKey(keyCode)) holdAction?.Invoke();
        }


        private KeyCode GetKeyCode(InputEnums input) {
            return input switch {
                InputEnums.MoveUp => _currentInputBinding.moveUp,
                InputEnums.MoveDown => _currentInputBinding.moveDown,
                InputEnums.RotateLeft => _currentInputBinding.rotateLeft,
                InputEnums.RotateRight => _currentInputBinding.rotateRight,
                InputEnums.Attack => _currentInputBinding.attack,
                InputEnums.NextWeapon => _currentInputBinding.nextWeapon,
                InputEnums.PreviousWeapon => _currentInputBinding.previousWeapon,
                _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
            };
        }
    }


    public static class InputBus {
        public static Action OnMoveUpPress;
        public static Action OnMoveDownPress;
        public static Action OnMoveLeftPress;
        public static Action OnMoveRightPress;
        public static Action OnAttackPress;
        public static Action OnNextWeaponPress;
        public static Action OnPreviousWeaponPress;

        public static Action OnMoveUpRelease;
        public static Action OnMoveDownRelease;
        public static Action OnMoveLeftRelease;
        public static Action OnMoveRightRelease;
        public static Action OnAttackRelease;
        public static Action OnNextWeaponRelease;
        public static Action OnPreviousWeaponRelease;

        public static Action OnMoveUpHold;
        public static Action OnMoveDownHold;
        public static Action OnMoveLeftHold;
        public static Action OnMoveRightHold;
        public static Action OnAttackHold;
        public static Action OnNextWeaponHold;
        public static Action OnPreviousWeaponHold;
    }

}