using Application.Interfaces;
using ProdMon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ProdMon.Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ArticleCodeRepository : IArticleCodeRepository
    {
        private readonly ProdMonDbContext _context;

        public ArticleCodeRepository(ProdMonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArticleCode>> GetAllArticleCodesAsync()
        {
            return await _context.ArticleCodes.ToListAsync();
        }

        public async Task<ArticleCode> GetArticleCodeByIdAsync(int id)
        {
            return await _context.ArticleCodes.FindAsync(id);
        }

        public async Task AddArticleCodeAsync(ArticleCode articleCode)
        {
            _context.ArticleCodes.Add(articleCode);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArticleCodeAsync(ArticleCode articleCode)
        {
            _context.Entry(articleCode).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticleCodeAsync(int id)
        {
            var articleCode = await _context.ArticleCodes.FindAsync(id);
            if (articleCode != null)
            {
                _context.ArticleCodes.Remove(articleCode);
                await _context.SaveChangesAsync();
            }
        }
    }
}
