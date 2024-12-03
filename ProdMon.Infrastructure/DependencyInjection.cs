using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProdMon.Infrastructure.Data;

namespace ProdMon.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // main DB context for use
            services.AddDbContext<ProdMonDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            // Register the IDbContextFactory with scoped lifetime for use on automated generated scaffolded .razor components for CRUD operations
            services.AddDbContextFactory<ProdMonDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            // Register the DatabaseCheckService
            services.AddScoped<DatabaseCheckService>();

            // Register the Entry Repo
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IArticleCodeRepository, ArticleCodeRepository>();

            return services;
        }
    }
}
