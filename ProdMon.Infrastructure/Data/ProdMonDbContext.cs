using Microsoft.EntityFrameworkCore;
using ProdMon.Domain.Models;

namespace ProdMon.Infrastructure.Data
{
    public class ProdMonDbContext : DbContext
    {
        public ProdMonDbContext(DbContextOptions<ProdMonDbContext> options) : base(options) { }
                
        public DbSet<ArticleCode> ArticleCodes { get; set; }
        public DbSet<MonitorEntry> MonitorEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // Seed some basic Data for ArticleCodes for later testing

            modelBuilder.Entity<ArticleCode>().HasData(
                new ArticleCode
                {
                    ArticleNumber = "55322234",
                    ArticleDescription = "Querlenker"
                },
                new ArticleCode
                {
                    ArticleNumber = "123455",
                    ArticleDescription = "Test"
                },
                new ArticleCode
                {
                    ArticleNumber = "05010292",
                    ArticleDescription = "Bremsscheibe"
                }
            );


        }

    }
}
