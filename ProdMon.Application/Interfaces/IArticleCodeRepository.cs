using ProdMon.Domain.Models;

namespace Application.Interfaces
{
    public interface IArticleCodeRepository
    {
        Task<IEnumerable<ArticleCode>> GetAllArticleCodesAsync();
        Task<ArticleCode> GetArticleCodeByIdAsync(int id);
        Task AddArticleCodeAsync(ArticleCode articleCode);
        Task UpdateArticleCodeAsync(ArticleCode articleCode);
        Task DeleteArticleCodeAsync(int id);
    }
}
