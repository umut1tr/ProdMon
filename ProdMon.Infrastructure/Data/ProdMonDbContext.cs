using Microsoft.EntityFrameworkCore;
using ProdMon.Domain.Models;

namespace ProdMon.Infrastructure.Data
{
    public class ProdMonDbContext : DbContext
    {
        public ProdMonDbContext(DbContextOptions<ProdMonDbContext> options) : base(options) { }
                
        public DbSet<ArticleCode> ArticleCodes { get; set; }
        public DbSet<MonitorEntry> MonitorEntries { get; set; }

        
    }
}
