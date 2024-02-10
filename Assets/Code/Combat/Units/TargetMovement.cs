using System;
using UnityEngine;

namespace Code.Combat.Units {

    public class TargetMovement : IMovement
    {
        public float Speed { get; private set; }
        private Transform _target;
        public Action<Vector3> OnMove { get; set; }
        
        private Func<Vector3> _positionGetter;
        private float stopDistance = 0.1f;
        
        public void SetPositionGetter(Func<Vector3> positionGetter) {
            _positionGetter = positionGetter;
        }
        public void SetTarget(Transform target) {
            _target = target;
        }
        
        public void SetSpeed(float speed) {
            Speed = speed;
        }
        public void SetStopDistance(float distance) {
            stopDistance = distance;
        }

        public void Move() {
            if (_target == null) return;
            var start = _positionGetter();
            var target = _target.position;
            var direction = (target - start).normalized;
            start += direction * Speed * Time.deltaTime;
            OnMove?.Invoke(start);
        }

        public bool IsAtTarget() {
            var start = _positionGetter();
            var target = _target.position;
            var distance = Vector3.Distance(start, target);
            return distance < stopDistance;
        }
    }

}