using Code.Combat.Data;

namespace Code.Combat {

    public class AttackState {
        public float Damage;
        public Team SourceTeam;
        public Context Context;

        public AttackState() {
            Damage = 0;
        }

        public AttackState GetCopy() {
            var copy = new AttackState {
                Damage = Damage,
                Context = Context.GetCopy(),
                SourceTeam = SourceTeam
            };

            return copy;
        }
        public AttackState GetCopy(Context ctx) {
            var copy = new AttackState {
                Damage = Damage,
                Context = ctx,
                SourceTeam = SourceTeam
            };
            return copy;
        }

        public AttackState(float damage) {
            Damage = damage;
            Context = new Context();
        }
    }

}