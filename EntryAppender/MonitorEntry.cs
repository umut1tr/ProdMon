using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class MonitorEntry
    {
        [JsonProperty("zeitstempel")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("dmc")]
        public string Dmc { get; set; }

        [JsonProperty("articleDescriptions")]
        public string Description { get; set; }

        [JsonProperty("qualitaet")]
        public string Quality { get; set; }
    }
}
