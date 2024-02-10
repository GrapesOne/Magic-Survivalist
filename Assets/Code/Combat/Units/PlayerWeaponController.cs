using Code.Combat.Data;
using Code.Combat.Units.Controllers;
using Code.Combat.Units.Entities;
using Code.Core;
using Code.Input;

namespace Code.Combat.Units {

    public class PlayerWeaponController {
        private IProjectileAggregator _projectileAggregator;
        private PlayerUnitController _playerUnitController;
        private PlayerEntity _playerEntity;
        private ProjectileData _currentProjectileData;
        private AttackState _attackState;

        public void Init(PlayerEntity unitEntity, PlayerUnitController unitController) {
            _playerUnitController = unitController;
            _playerEntity = unitEntity;
            InputBus.OnAttackPress += Attack;
            InputBus.OnNextWeaponPress += NextWeapon;
            InputBus.OnPreviousWeaponPress += PreviousWeapon;
        }

        public void Dispose() {
            _playerUnitController = null;
            _playerEntity = null;
            _projectileAggregator = null;
            _currentProjectileData = null;
            _attackState = null;
            InputBus.OnAttackPress -= Attack;
            InputBus.OnNextWeaponPress -= NextWeapon;
            InputBus.OnPreviousWeaponPress -= PreviousWeapon;
        }

        public void SetProjectileAggregator(IProjectileAggregator projectileAggregator) {
            _projectileAggregator = projectileAggregator;
        }

        private void PreviousWeapon() {
            UpdateCurrentProjectileData(_projectileAggregator.GetPreviousProjectile());
        }

        private void NextWeapon() {
            UpdateCurrentProjectileData(_projectileAggregator.GetNextProjectile());
        }

        public void SetProjectileData(ProjectileData standardProjectileData) {
            UpdateCurrentProjectileData(standardProjectileData);
        }

        private void UpdateCurrentProjectileData(ProjectileData projectileData) {
            _currentProjectileData = projectileData;
            _attackState = new AttackState(_currentProjectileData.attackDamage);
            _attackState.Context.SetSource(_playerEntity);
            _attackState.SourceTeam = Team.Player;
            EventBus.OnProjectileSelected?.Invoke(_currentProjectileData);
        }

        private void Attack() {
            EventBus.SpawnProjectile?.Invoke(_currentProjectileData, _playerUnitController.GetPosition(),
                _playerUnitController.GetRotation(), OnProjectileSpawned);
        }

        private void OnProjectileSpawned(ProjectileEntity projectile) {
            projectile.SetAttackState(_attackState.GetCopy());
        }
    }

}