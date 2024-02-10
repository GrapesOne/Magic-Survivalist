using NaughtyAttributes;
using UnityEngine;

namespace Code.Combat.Units {

    public class HealthController {
        public float MaxHealth;
        public float CurrentHealth;
        public float Shield;
        public bool IsDead;

        private IDamageable _damageable;

        private const string LogFormat = "{0} dealt {1} damage to {2} current health: {3}";
        
        public void Init(IDamageable damageable, float maxHealth, float shield) {
            _damageable = damageable;
            Shield = shield;
            SetMaxHealth(maxHealth, needToSetHpToMax: true);
        }

        public void SetHpToMax() {
            CurrentHealth = MaxHealth;
            IsDead = false;
            _damageable.OnHealthChanged?.Invoke();
        }

        public void SetMaxHealth(float value, bool needToSetHpToMax = false) {
            MaxHealth = value;
            if (needToSetHpToMax) SetHpToMax();
            _damageable.OnMaxHealthChanged?.Invoke();
        }

        public float GetMaxHealth() => MaxHealth;
        public float GetCurrentHealth() => CurrentHealth;

        public void TakeDamage(float damage, Context context) {
            if (IsDead) return;
            var damageToTake = Mathf.Clamp(damage - Shield, 0, float.MaxValue);
            CurrentHealth -= damageToTake;
            Debug.Log(string.Format(LogFormat, context.SourceName, damageToTake, context.ReceiverName, CurrentHealth));
            _damageable.OnHealthChanged?.Invoke();
            if (CurrentHealth > 0) return;
            Die(context);
        }

        private void Die(Context context = default) {
            IsDead = true;
            _damageable.Die(context);
        }


        [Button]
        public void Kill() {
            Die();
        }
    }

}