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
            

            services.AddDbContext<ProdMonDbContext>(options =>
                options.UseSqlServer(connectionString) 
            );

            // Register the DatabaseCheckService
            services.AddScoped<DatabaseCheckService>();

            // Register the Entry Repo
            services.AddScoped<IEntryRepository, EntryRepository>();

            return services;
        }
    }
}
