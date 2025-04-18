using Newtonsoft.Json.Linq;

namespace _Migration
{
    public interface IMigration
    {
        string FromVersion { get; }
        string ToVersion { get; }
        
        JObject Migrate(JObject data);
    }
}