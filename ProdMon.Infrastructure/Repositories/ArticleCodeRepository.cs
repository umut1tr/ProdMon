using Microsoft.EntityFrameworkCore;
using ProdMon.Domain.Models;
using ProdMon.Infrastructure.Data;

public class ArticleCodeRepository : IArticleCodeRepository
{
    private readonly ProdMonDbContext _context;

    public ArticleCodeRepository(ProdMonDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArticleCode>> GetAllAsync()
    {
        return await _context.ArticleCodes.ToListAsync();
    }

    public async Task<ArticleCode> GetByIdAsync(string id)
    {
        return await _context.ArticleCodes.FindAsync(id);
    }

    public async Task AddAsync(ArticleCode articleCode)
    {
        _context.ArticleCodes.Add(articleCode);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ArticleCode articleCode)
    {
        _context.ArticleCodes.Update(articleCode);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ArticleCode articleCode)
    {
        _context.ArticleCodes.Remove(articleCode);
        await _context.SaveChangesAsync();
    }
}
