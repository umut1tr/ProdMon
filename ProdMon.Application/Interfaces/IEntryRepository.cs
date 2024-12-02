using System.Collections.Generic;
using System.Threading.Tasks;
using ProdMon.Domain.Models;

namespace Application.Interfaces
{
    public interface IEntryRepository
    {
        Task<IEnumerable<MonitorEntry>> GetAllEntriesAsync();
        Task<MonitorEntry> GetEntryByIdAsync(string id);  // Changed to string
        Task AddEntryAsync(MonitorEntry entry);
        Task UpdateEntryAsync(MonitorEntry entry);
        Task DeleteEntryAsync(string id);  // Changed to string
    }
}
