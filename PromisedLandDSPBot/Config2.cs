using Newtonsoft.Json;

namespace PromisedLandDSPBot;

public class Config2
{
    //per application config class
    class application
    {
        application(string path = "config.json")
        {
            
        }
        
        [JsonProperty] private string[] whitelist;
        [JsonProperty] private string[] blacklist;
        [JsonProperty] string DiscordToken;
        [JsonProperty] private string GPT3Token;
    }

    //per guild config class
    class guild
    {
        guild(ulong ID)
        {
            
        }

        public Task<ulong> getLogChannel()
        {
            
        }
        public Task setLogChannel(ulong ID)
        {
            
            return Task.CompletedTask;
        }
        

        public Task<ulong> getDebug()
        {
            
        }
        public Task setDebugChannel(ulong ID)
        {
         
            return Task.CompletedTask;
        }

        
        public Task<ulong> getAnnouncementChannel()
        {
            
        }
        public Task setAnnouncementChannel()
        {
            
            return Task.CompletedTask;
        }


        [JsonProperty] private ulong log;
        [JsonProperty] private ulong debug;
        [JsonProperty] private ulong announcement;
    }
}