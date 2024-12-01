using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMon.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // eventuelle services später? Einträge des Fremdsystems + Artikelcodes für "Artikelnummer (Key) , + Produktname)??

            // services.AddScoped<IQualityControlEntryRepository, QualityControlEntryRepository>();
            // services.AddScoped<IArtikelcodeRepository, ArtikelCodeRepository>();

            return services;
        }


    }
}
