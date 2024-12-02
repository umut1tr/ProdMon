using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProdMon.Application.Interfaces;
using ProdMon.Application.Services;

namespace ProdMon.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, string filePath, string lastCheckedFilePath)
        {
            services.AddSingleton<IFileWatcherService>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<FileWatcherService>>();
                var serviceScopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
                return new FileWatcherService(filePath, lastCheckedFilePath, logger, serviceScopeFactory);
            });

            services.AddHostedService(provider => (FileWatcherService)provider.GetRequiredService<IFileWatcherService>());

            // Other service registrations...

            return services;
        }
    }
}
