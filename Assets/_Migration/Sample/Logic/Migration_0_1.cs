using Newtonsoft.Json.Linq;

namespace _Migration
{
    public class Migration_0_1 : IMigration
    {
        public string FromVersion => "0.0.0";
        public string ToVersion => "0.1.0";
        
        public JObject Migrate(JObject data)
        {
            string fullName = data["PlayerName"].ToString();
            string[] names = fullName.Split(' ');

            data.Remove("PlayerName");
            
            data["FirstName"] = names[0];
            data["LastName"] = names[1];

            return data;
        }
    }
}