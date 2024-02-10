using System;
using UnityEngine;

namespace Code.Combat.Units {

    public class SimpleMovement : IMovement
    {
        public float Speed { get; private set; }
        private Vector3 _direction;
        public Action<Vector3> OnMove { get; set; }
        
        private Func<Vector3> _positionGetter;
        
        public void SetPositionGetter(Func<Vector3> positionGetter) {
            _positionGetter = positionGetter;
        }
        public void SetDirection(Vector3 direction) {
            this._direction = direction;
        }
        
        public void SetSpeed(float speed) {
            Speed = speed;
        }

        public void Move() {
            var start = _positionGetter();
            start += _direction * Speed * Time.deltaTime;
            OnMove?.Invoke(start);
        }
    }

}
