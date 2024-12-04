using Microsoft.AspNetCore.Mvc;
using ProdMon.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ArticleCodesController : ControllerBase
{
    private readonly IArticleCodeRepository _repository;

    public ArticleCodesController(IArticleCodeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticleCodesAsync()
    {
        var articleCodes = await _repository.GetAllAsync();
        return Ok(articleCodes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArticleCodeByIdAsync(string id)
    {
        var articleCode = await _repository.GetByIdAsync(id);
        if (articleCode == null)
        {
            return NotFound();
        }
        return Ok(articleCode);
    }

    [HttpPost]
    public async Task<IActionResult> AddArticleCodeAsync([FromBody] ArticleCode articleCode)
    {
        await _repository.AddAsync(articleCode);
        return CreatedAtAction(nameof(GetArticleCodeByIdAsync), new { id = articleCode.ArticleNumber }, articleCode);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArticleCodeAsync(string id, [FromBody] ArticleCode articleCode)
    {
        if (id != articleCode.ArticleNumber)
        {
            return BadRequest();
        }

        await _repository.UpdateAsync(articleCode);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticleCodeAsync(string id)
    {
        var articleCode = await _repository.GetByIdAsync(id);
        if (articleCode == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(articleCode);
        return NoContent();
    }
}
