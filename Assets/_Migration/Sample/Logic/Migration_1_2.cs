using Newtonsoft.Json.Linq;

namespace _Migration
{
    public class Migration_1_2 : IMigration
    {
        public string FromVersion => "0.1.0";
        public string ToVersion => "0.2.0";
        
        public JObject Migrate(JObject data)
        {
            int woodAmount = data["Wood"]?.Value<int>() ?? 0;
            int stoneAmount = data["Stone"]?.Value<int>() ?? 0;
            JArray resources = data["Resources"] as JArray ?? new JArray();

            resources.Add(JObject.FromObject(new { Name = "Wood", Amount = woodAmount }));
            resources.Add(JObject.FromObject(new { Name = "Stone", Amount = stoneAmount }));
            
            data["Resources"] = resources;
            
            data.Remove("Wood");
            data.Remove("Stone");

            return data;
        }
    }
}