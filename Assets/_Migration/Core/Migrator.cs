using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Migration
{
    public class Migrator
    {
        private readonly string _targetVersion;
        private readonly IOrderedEnumerable<IMigration> _migrations;

        public Migrator(string targetVersion, params IMigration[] migrations)
        {
            _targetVersion = targetVersion;
            _migrations = migrations.OrderBy(x => x.ToVersion);
        }

        public T Execute<T>(string rawData)
        {
            JObject data = JObject.Parse(rawData);
            
            if (data["Version"]!.ToString() == _targetVersion)
                return Parse<T>(data);
            
            foreach (IMigration migration in _migrations)
            {
                Version version = new(data["Version"]!.ToString());
                
                if (version.CompareTo(migration.ToVersion) < 0)
                {
                    data = migration.Migrate(data);
                    data["Version"] = migration.ToVersion.ToString();
                }
            }
            
            return Parse<T>(data);
        }
        
        private T Parse<T>(JObject data) => 
            JsonConvert.DeserializeObject<T>(data.ToString());
    }
}