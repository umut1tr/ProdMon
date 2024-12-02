using Application.Interfaces;
using ProdMon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ProdMon.Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly ProdMonDbContext _context;

        public EntryRepository(ProdMonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonitorEntry>> GetAllEntriesAsync()
        {
            return await _context.MonitorEntries.ToListAsync();
        }

        public async Task<MonitorEntry> GetEntryByIdAsync(string id)
        {
            return await _context.MonitorEntries.FindAsync(id);
        }

        public async Task AddEntryAsync(MonitorEntry entry)
        {
            _context.MonitorEntries.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntryAsync(MonitorEntry entry)
        {
            _context.Entry(entry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntryAsync(string id)
        {
            var entry = await _context.MonitorEntries.FindAsync(id);
            if (entry != null)
            {
                _context.MonitorEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
