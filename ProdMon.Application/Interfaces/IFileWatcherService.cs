
using ProdMon.Domain.Models;

namespace ProdMon.Application.Interfaces
{
    public interface IFileWatcherService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);

        //// Event, das ausgelöst wird, wenn sich die Datei geändert hat um dem FrontEnd zu sagen, dass es neu laden soll        
        event Action? EntryHasBeenApplied;
    }
}
