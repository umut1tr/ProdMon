
using ProdMon.Domain.Models;

namespace ProdMon.Application.Interfaces
{
    public interface IFileWatcherService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);

        // Signal to FrontEnd   
        event Action? EntryHasBeenApplied;
    }
}
