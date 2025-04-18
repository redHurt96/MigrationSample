using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Migration
{
    public class Migrator
    {
        private readonly string _targetVersion;
        private readonly IMigration[] _migrations;

        public Migrator(string targetVersion, params IMigration[] migrations)
        {
            _targetVersion = targetVersion;
            _migrations = migrations;
        }

        public T Execute<T>(string rawData)
        {
            JObject data = JObject.Parse(rawData);
            
            if (data["Version"]!.ToString() == _targetVersion)
                return JsonConvert.DeserializeObject<T>(data.ToString());
            
            foreach (IMigration migration in _migrations.OrderBy(x => x.FromVersion))
            {
                string version = data["Version"]!.ToString();
                
                if (version == migration.FromVersion)
                {
                    data = migration.Migrate(data);
                    data["Version"] = migration.ToVersion;
                }
            }
            
            return JsonConvert.DeserializeObject<T>(data.ToString());
        }
    }
}