using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Combat.Data {

    [CreateAssetMenu(fileName = "PlayerCollection", menuName = "Combat/PlayerCollection", order = 1)]
    public class PlayerCollection : ScriptableObject, IDataCollection {
        public const string Path = "ScriptableObjects/Collections/PlayerCollection";

        [field: SerializeField, Expandable] private List<PlayerData> players;
        private PlayerData _lastPlayer;

        public PlayerData GetPlayer(int id) => GetUnitData(id) as PlayerData;
        public PlayerData GetLastPlayerOrFirst() => GetCurrentDataOrFirst() as PlayerData;
        public PlayerData GetRandomPlayer() => GetRandomData() as PlayerData;

        public BaseUnitCombatData GetUnitData(int id) {
            foreach (var player in players) {
                if (player.id != id) continue;
                _lastPlayer = player;
                return player;
            }

            throw new System.Exception($"Player with id {id} not found");
        }

        public BaseUnitCombatData GetCurrentDataOrFirst() {
            if (_lastPlayer == null) _lastPlayer = players[0];
            return _lastPlayer;
        }

        public BaseUnitCombatData GetRandomData() {
            return players[Random.Range(0, players.Count)];
        }
    }

}