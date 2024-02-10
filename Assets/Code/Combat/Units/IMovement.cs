using System;
using UnityEngine;

namespace Code.Combat.Units {

   public interface IMovement 
   {
      float Speed { get; }
      Action<Vector3> OnMove { get; set; }
      void SetSpeed(float speed);
      void Move();
   
   }

}
