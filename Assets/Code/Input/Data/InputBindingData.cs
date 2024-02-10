using UnityEngine;

namespace Code.Input.Data {

    [CreateAssetMenu(fileName = "InputBindingData", menuName = "Input/Input Binding Data")]
    public class InputBindingData : ScriptableObject {
        public InputType inputType;
        [Space]
        public KeyCode moveUp;
        public KeyCode moveDown;
        public KeyCode rotateLeft;
        public KeyCode rotateRight;
        public KeyCode attack;
        public KeyCode nextWeapon;
        public KeyCode previousWeapon;
    }

}