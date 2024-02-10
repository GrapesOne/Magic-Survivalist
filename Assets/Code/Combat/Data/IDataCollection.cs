namespace Code.Combat.Data {

    public interface IDataCollection 
    {
        BaseUnitCombatData GetUnitData(int id);
        BaseUnitCombatData GetCurrentDataOrFirst();
        BaseUnitCombatData GetRandomData();
    }

}
