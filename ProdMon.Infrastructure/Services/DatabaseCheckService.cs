using ProdMon.Infrastructure.Data;

public class DatabaseCheckService
{
    private readonly ProdMonDbContext _context;

    public DatabaseCheckService(ProdMonDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CanConnectAsync()
    {
        try
        {
            return await _context.Database.CanConnectAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to the database: {ex.Message}");
            return false;
        }
    }
}
