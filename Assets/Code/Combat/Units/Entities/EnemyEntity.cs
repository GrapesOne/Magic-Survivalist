using System;
using Code.Combat.Data;
using Code.Combat.Units.Behaviours;
using Code.Combat.Units.Controllers;
using Code.Core;

namespace Code.Combat.Units.Entities {

    public class EnemyEntity : UnitEntity<EnemyUnitController, EnemyData>, IDamageable {
        private EnemyBehaviour _enemyBehaviour;
        private HealthController _healthController;
        public AttackState AttackState { get; set; }

        public override void Init() {
            base.Init();
            Team = Team.Enemy;
            _enemyBehaviour = new EnemyBehaviour();
            _enemyBehaviour.Init(this, Controller);
            _healthController = new HealthController();
            _healthController.Init(this, Data.maxHealth, Data.shield);
            EventBus.RegisterBehaviour?.Invoke(_enemyBehaviour);
            AttackState = new AttackState(Data.attackDamage);
            AttackState.Context.SetSource(this);
            AttackState.SourceTeam = Team.Enemy;
        }

        #region IDamagable

        public IDamageable AsDamageable => this;

        public float CurrentHealth {
            get => _healthController.CurrentHealth;
            set => _healthController.CurrentHealth = value;
        }

        public float MaxHealth => _healthController.GetMaxHealth();

        public bool IsDead {
            get => _healthController.IsDead;
            set => _healthController.IsDead = value;
        }

        public Action OnHealthChanged { get; set; }
        public Action<IDamageable, bool> OnDying { get; set; }
        public Action OnMaxHealthChanged { get; set; }

        #endregion

        public override void HandleCollision(AttackState attackState) {
            TakeDamage(attackState.Damage, attackState.Context);
        }

        public void TakeDamage(float damage, Context context) {
            _healthController.TakeDamage(damage, context);
        }

        public void Die(Context context = default) {
            OnDying?.Invoke(this, true);
            _enemyBehaviour.Dispose();
            _enemyBehaviour = null;
            Dispose();
        }
    }

}