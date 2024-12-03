using ProdMon.Domain.Models;
using System.ComponentModel.DataAnnotations;


namespace ProdMon.Domain.Models
{
    public class ArticleCode
    {
        [Key]
        public string ArticleNumber { get; set; }
        [Required]
        public string ArticleDescription { get; set; }
    }
}


