using System;
using Code.Combat.Data;
using Code.Combat.Units.Behaviours;
using Code.Combat.Units.Controllers;
using Code.Core;

namespace Code.Combat.Units.Entities {

    public class PlayerEntity : UnitEntity<PlayerUnitController, PlayerData>, IDamageable {
        private PlayerBehaviour _playerBehaviour;
        private PlayerWeaponController _playerWeaponController;
        private HealthController _healthController;
        
        public override void Init() {
            base.Init();
            Team = Team.Player;
            _healthController = new HealthController();
            _healthController.Init(this, Data.maxHealth, Data.shield);
            _playerBehaviour = new PlayerBehaviour();
            _playerBehaviour.Init(this, Controller);
            _playerWeaponController = new PlayerWeaponController();
            _playerWeaponController.Init(this, Controller);
        }

        public void SetProjectileData(ProjectileData standardProjectileData) =>
            _playerWeaponController.SetProjectileData(standardProjectileData);

        public void SetProjectileAggregator(IProjectileAggregator projectileAggregator) =>
            _playerWeaponController.SetProjectileAggregator(projectileAggregator);
        

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
            _playerBehaviour.Dispose();
            _playerBehaviour = null;
            _playerWeaponController.Dispose();
            _playerWeaponController = null;
            EventBus.OnPlayerDeath?.Invoke();
            Dispose();
        }
    }

}