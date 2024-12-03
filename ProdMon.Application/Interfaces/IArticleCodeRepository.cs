using ProdMon.Domain.Models;

public interface IArticleCodeRepository
{
    Task<List<ArticleCode>> GetAllAsync();
    Task<ArticleCode> GetByIdAsync(string id);
    Task AddAsync(ArticleCode articleCode);
    Task UpdateAsync(ArticleCode articleCode);
    Task DeleteAsync(ArticleCode articleCode);
}
