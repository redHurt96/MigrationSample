using System;
using Newtonsoft.Json.Linq;

namespace _Migration
{
    public interface IMigration
    {
        Version ToVersion { get; }
        JObject Migrate(JObject data);
    }
}