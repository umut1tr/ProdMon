using System.ComponentModel.DataAnnotations;

namespace ProdMon.Domain.Models
{
    public class MonitorEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Dmc { get; set; }
        public string Description { get; set; }
        public string Quality { get; set; }
    }
}

