using Code.Combat.Units.Controllers;
using Code.Combat.Units.Entities;
using Code.Core;

namespace Code.Combat.Units.Behaviours {

    public abstract class UnitBehaviour 
    {
        protected UnitEntity UnitEntity;
        protected UnitController UnitController;
        public virtual void Init(UnitEntity unitEntity, UnitController unitController)
        {
            UnitEntity = unitEntity;
            UnitController = unitController;
        }

        public virtual void Dispose()
        {
            EventBus.UnregisterBehaviour?.Invoke(this);
            UnitEntity = null;
            UnitController = null;
        }
        public abstract void OnUpdate();
    }

}
