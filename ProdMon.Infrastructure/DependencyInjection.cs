using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProdMon.Infrastructure.Data;

namespace ProdMon.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // eventuelle services später?
            // services.AddScoped<IQualityControlEntryRepository, QualityControlEntryRepository>();
            // services.AddScoped<IArtikelcodeRepository, ArtikelCodeRepository>();

            services.AddDbContext<ProdMonDbContext>(options =>
                options.UseSqlServer(connectionString) 
            );

            // Registriere den DatabaseCheckService
            services.AddScoped<DatabaseCheckService>();

            return services;
        }
    }
}
