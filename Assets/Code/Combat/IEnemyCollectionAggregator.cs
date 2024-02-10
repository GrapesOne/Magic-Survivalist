using Code.Combat.Data;

namespace Code.Combat {

    public interface IEnemyCollectionAggregator {
        EnemyData GetEnemyData(int id);
        EnemyData GetCurrentEnemyOrFirst();
        EnemyData GetRandomEnemy();
    }

}
