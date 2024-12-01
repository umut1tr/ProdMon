
using ProdMon.Infrastructure.Data;

namespace ProdMon.Infrastructure.Services
{
    public class DatabaseCheckService
    {
        private readonly ProdMonDbContext _context;

        public DatabaseCheckService(ProdMonDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanConnectAsync()
        {
            return await _context.Database.CanConnectAsync();
        }
    }
}
