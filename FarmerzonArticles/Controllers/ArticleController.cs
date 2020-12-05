using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmerzonArticlesManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticles.Controllers
{
    [Authorize]
    [Route("article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleManager ArticleManager { get; set; }

        public ArticleController(IArticleManager articleManager)
        {
            ArticleManager = articleManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.ArticleOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostArticleAsync([FromBody] DTO.ArticleInput article)
        {
            var userName = User.FindFirst("userName")?.Value;
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var insertedArticle = await ArticleManager.InsertEntityAsync(article, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<DTO.ArticleOutput>
            {
                Success = true,
                Content = insertedArticle
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.ArticleOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesAsync([FromQuery] long? articleId, [FromQuery] string name,
            [FromQuery] string description, [FromQuery] double? price, [FromQuery] int? amount,
            [FromQuery] double? size, [FromQuery] DateTime? createdAt, [FromQuery] DateTime? updatedAt,
            [FromQuery] DateTime? expirationDate)
        {
            var articles = await ArticleManager.GetEntitiesAsync(articleId, name, description, price,
                amount, size, createdAt, updatedAt, expirationDate);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.ArticleOutput>>
            {
                Success = true,
                Content = articles
            });
        }

        [HttpPost("get-by-normalized-user-name")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IEnumerable<DTO.ArticleOutput>>>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByNormalizedUsernameAsync(
            [FromBody] IEnumerable<string> normalizedUserNames)
        {
            var articles = await ArticleManager.GetEntitiesByNormalizedUserNameAsync(normalizedUserNames);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IEnumerable<DTO.ArticleOutput>>>
            {
                Success = true,
                Content = articles
            });
        }

        [HttpPost("get-by-unit-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IEnumerable<DTO.ArticleOutput>>>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByUnitIdAsync([FromBody] IEnumerable<long> unitIds)
        {
            var articles = await ArticleManager.GetEntitiesByUnitIdAsync(unitIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IEnumerable<DTO.ArticleOutput>>>
            {
                Success = true,
                Content = articles
            });
        }

        [HttpGet("get-by-expiration-date")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.ArticleOutput>>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByExpirationDateAsync([FromQuery] int amount)
        {
            var articles = await ArticleManager.GetEntitiesByExpirationDateAsync(amount);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.ArticleOutput>>
            {
                Success = true,
                Content = articles
            });
        }

        [HttpPut]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.ArticleOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateArticleAsync([FromQuery] long articleId,
            [FromBody] DTO.ArticleInput article)
        {
            var userName = User.FindFirst("userName")?.Value;
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var updatedArticle =
                await ArticleManager.UpdateEntityAsync(articleId, article, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<DTO.ArticleOutput>
            {
                Success = true,
                Content = updatedArticle
            });
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.ArticleOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteArticleAsync([FromQuery] long articleId)
        {
            var userName = User.FindFirst("userName")?.Value;
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var deletedArticle = await ArticleManager.RemoveEntityByIdAsync(articleId, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<DTO.ArticleOutput>
            {
                Success = true,
                Content = deletedArticle
            });
        }
    }
}