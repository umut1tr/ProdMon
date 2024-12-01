using System.ComponentModel.DataAnnotations;


namespace ProdMon.Domain.Models
{
    public class ArticleCode
    {
        [Key]
        public int ArticleNumber { get; set; }
        public string ArticleDescription { get; set; }
    }
}
