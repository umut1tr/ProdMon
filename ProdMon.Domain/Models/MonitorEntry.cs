using System.ComponentModel.DataAnnotations;

namespace ProdMon.Domain.Models
{
    public class MonitorEntry
    {
        [Key]
        public long Dmc { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Quality { get; set; }
        public int CheckPointId { get; set; } = 17;
    }
}

