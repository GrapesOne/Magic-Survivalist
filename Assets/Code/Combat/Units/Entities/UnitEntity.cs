using System;
using Code.Combat.Data;
using Code.Combat.Units.Controllers;
using Code.Core;
using UnityEngine;

namespace Code.Combat.Units.Entities {

    public abstract class UnitEntity<T, U> : UnitEntity where T : UnitController where U : BaseUnitCombatData {
        public T Controller { get; private set; }
        public U Data { get; private set; }

        protected bool IsInitialized;

        public virtual void Init() {
            if (IsInitialized) return;
            if (Controller != null && Data != null) IsInitialized = true;
        }

        public virtual void Dispose() {
            EventBus.OnUnitEntityDisposed?.Invoke(this);
            Controller.Dispose();
            Data = null;
        }

        public virtual void SetController(T controller) {
            Controller = controller;
            CallOnSetController(controller);
        }

        public void SetData(U data) {
            Data = data;
        }

        public override Vector3 GetPosition() => Controller.GetPosition();
        public override string GetName() => Controller.name;
        public override Collider Collider => Controller.Collider;
    }

    public abstract class UnitEntity {
        public Team Team { get; protected set; }
        protected void CallOnSetController(UnitController controller) {
            OnSetController?.Invoke(controller);
            OnSetController = null;
        }

        public event Action<UnitController> OnSetController;
        public abstract Collider Collider { get; }
        public abstract Vector3 GetPosition();
        public abstract string GetName();
        public abstract void HandleCollision(AttackState attackState);
    }

}