using System.Reflection;
using Newtonsoft.Json;
using PromisedLandDSPBot.Functions.Config.Objects;

namespace PromisedLandDSPBot;

public class Config2
{
    //per application config class
    class application
    {
        private string path;
        application(string path = "config.json")
        {
            this.path = path;
            
            if (File.Exists(path))
            {
                // load config from file
                var jStr = File.ReadAllTextAsync(path);
            
                // deserialize config, populate this class
                JsonConvert.PopulateObject(jStr.Result, this);
            }
            else
            {
                // create new config file
                var jStr = JsonConvert.SerializeObject(this, Formatting.Indented);
                
                // write config to file
                File.WriteAllTextAsync(path, jStr);
                
                // load config from created file
                JsonConvert.PopulateObject(jStr, this);
            }

        }
        
        
        [JsonProperty] private HashSet<Guild> _guilds = new HashSet<Guild>();
        [JsonProperty] private string _token = "put your Discord token here";
        public string GetToken(bool serialize = false)
        {
            if (!serialize) return _token;

            Serialize(this, this.path);

            return _token;
        }
        
        public void SetToken(string token, bool serialize = true)
        {
            this._token = token;
            Serialize(this, this.path);
        }

        private void Serialize(application application, string path, Formatting formatting = Formatting.Indented)
        {
            string jStr = JsonConvert.SerializeObject(this, formatting);
            File.WriteAllTextAsync(path, jStr);
        }

    }

    //per guild config class
    class guild
    {
        [JsonProperty] private ulong Id;
        [JsonProperty] private List<KeyValuePair<string, string>> Fields;
        
        public KeyValuePair<string, string> Get(string Key, bool writeToDisk = false)
        {
            return Enumerable.First(Fields, entry => entry.Key == Key);
        }
        
        public void Set(KeyValuePair<string, string> entry, bool writeToDisk = true)
        {
            if(Fields.TrueForAll(entry => entry.Key != entry.Key))
            {
                Fields.Add(entry);
            }
            else
            {
                Fields[Fields.FindIndex(entry => entry.Key == entry.Key)] = entry;
            }
        }
    }
}