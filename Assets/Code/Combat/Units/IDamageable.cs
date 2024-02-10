using System;

namespace Code.Combat.Units {

    public interface IDamageable {
        float CurrentHealth { get; set; }
        float MaxHealth { get; }

        bool IsDead { get; set; }

        Action OnHealthChanged { get; set; }
        Action<IDamageable, bool> OnDying { get; set; }
        Action OnMaxHealthChanged { get; set; }

        void TakeDamage(float damage, Context context);

        void Die(Context context = default);
    }

}