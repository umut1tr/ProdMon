using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ProdMon.Domain.Models
{
    public class MonitorEntry
    {
        [Key]
        [JsonProperty("dmc")]
        public string Dmc { get; set; }
        [Required]
        [JsonProperty("zeitstempel")]
        public DateTime Timestamp { get; set; }
        [Required]
        [JsonProperty("articleDescriptions")]
        public string Description { get; set; }
        [Required]
        [JsonProperty("qualitaet")]
        public string Quality { get; set; }
        public int CheckPointId { get; set; } = 17;
    }
}

