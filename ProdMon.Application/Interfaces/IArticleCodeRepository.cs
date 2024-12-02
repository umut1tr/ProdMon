using ProdMon.Domain.Models;

namespace Application.Interfaces
{
    public interface IArticleCodeRepository
    {
        Task<List<ArticleCode>> GetAllAsync();
        Task<ArticleCode> GetByIdAsync(int id);
        Task AddAsync(ArticleCode articleCode);
        Task UpdateAsync(ArticleCode articleCode);
        Task DeleteAsync(ArticleCode articleCode);
    }
}
