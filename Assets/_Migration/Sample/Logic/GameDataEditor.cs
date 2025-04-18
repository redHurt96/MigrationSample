using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Migration
{
    public class GameDataEditor : MonoBehaviour
    {
        public string CurrentVersion;
        
        [SerializeField] private GameData _gameData;
        
        private string Path => System.IO.Path.Combine(Application.streamingAssetsPath, "GameData.json");
        
        [Button]
        private void Save() =>
            File.WriteAllText(Path, JsonConvert.SerializeObject(_gameData));

        [Button]
        private void Load()
        {
            string json = File.ReadAllText(Path);
            _gameData = new Migrator(
                    CurrentVersion, 
                    new Migration_0_1(),
                    new Migration_1_2())
                .Execute<GameData>(json);
        }
    }
}