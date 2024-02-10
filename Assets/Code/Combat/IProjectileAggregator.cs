using Code.Combat.Data;

namespace Code.Combat {

    public interface IProjectileAggregator {
        ProjectileData GetProjectileData(int id);
        ProjectileData GetCurrentProjectileOrFirst();
        ProjectileData GetRandomProjectile();
        ProjectileData GetNextProjectile();
        ProjectileData GetPreviousProjectile();
    }

}
