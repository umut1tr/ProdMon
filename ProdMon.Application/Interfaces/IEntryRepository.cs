using ProdMon.Domain.Models;

namespace Application.Interfaces
{
    public interface IEntryRepository
    {
        Task<IEnumerable<MonitorEntry>> GetAllEntriesAsync();
        Task<MonitorEntry> GetEntryByIdAsync(string id);
        Task AddEntryAsync(MonitorEntry entry);
        Task UpdateEntryAsync(MonitorEntry entry);
        Task DeleteEntryAsync(string id);
    }
}
