using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using ProdMon.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdMon.WebUi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCodeController : ControllerBase
    {
        private readonly IArticleCodeRepository _articleCodeRepository;

        public ArticleCodeController(IArticleCodeRepository articleCodeRepository)
        {
            _articleCodeRepository = articleCodeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleCode>>> GetArticleCodes()
        {
            var articleCodes = await _articleCodeRepository.GetAllArticleCodesAsync();
            return Ok(articleCodes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleCode>> GetArticleCode(int id)
        {
            var articleCode = await _articleCodeRepository.GetArticleCodeByIdAsync(id);
            if (articleCode == null)
            {
                return NotFound();
            }
            return Ok(articleCode);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleCode>> PostArticleCode(ArticleCode articleCode)
        {
            await _articleCodeRepository.AddArticleCodeAsync(articleCode);
            return CreatedAtAction(nameof(GetArticleCode), new { id = articleCode.ArticleNumber }, articleCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleCode(int id, ArticleCode articleCode)
        {
            if (id != articleCode.ArticleNumber)
            {
                return BadRequest();
            }

            await _articleCodeRepository.UpdateArticleCodeAsync(articleCode);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleCode(int id)
        {
            await _articleCodeRepository.DeleteArticleCodeAsync(id);
            return NoContent();
        }
    }
}
