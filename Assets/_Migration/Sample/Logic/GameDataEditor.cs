using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Migration
{
    public class GameDataEditor : MonoBehaviour
    {
        public string CurrentVersion;
        private const string KEY = "GameData";
        
        [SerializeField] private GameData _gameData;
        
        [Button]
        private void Save() => 
            PlayerPrefs.SetString(KEY, JsonConvert.SerializeObject(_gameData));

        [Button]
        private void Load() => 
            _gameData = new Migrator(CurrentVersion)
                .Execute<GameData>(PlayerPrefs.GetString(KEY));
    }
}