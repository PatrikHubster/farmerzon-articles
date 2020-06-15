using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// Request a list of articles.
        /// </summary>
        /// <param name="articleId">Optional parameter for querying for articles.</param>
        /// <param name="name">Optional parameter for querying for articles.</param>
        /// <param name="description">Optional parameter for querying for articles.</param>
        /// <param name="price">Optional parameter for querying for articles.</param>
        /// <param name="amount">Optional parameter for querying for articles.</param>
        /// <param name="size">Optional parameter for querying for articles.</param>
        /// <param name="createdAt">Optional parameter for querying for articles.</param>
        /// <param name="updatedAt">Optional parameter for querying for articles.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.ListResponse<DTO.Article>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleAsync([FromQuery]long? articleId, [FromQuery]string name,
            [FromQuery]string description, [FromQuery]double? price, [FromQuery]int? amount, [FromQuery]double? size,
            [FromQuery]DateTime? createdAt, [FromQuery]DateTime? updatedAt)
        {
            var articles = await ArticleManager.GetEntitiesAsync(articleId, name, description, price, 
                amount, size, createdAt, updatedAt);
            return Ok(new DTO.ListResponse<DTO.Article>
            {
                Success = true,
                Content = articles
            });
        }
        
        /// <summary>
        /// Request a list of articles.
        /// </summary>
        /// <param name="normalizedUserNames">Find articles to the listed normalized user names.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("get-by-normalized-user-name")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<IList<DTO.Article>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByPersonIdAsync([FromQuery]IEnumerable<string> normalizedUserNames)
        {
            var articles = await ArticleManager.GetArticlesByNormalizedUserNameAsync(normalizedUserNames);
            return Ok(new DTO.DictionaryResponse<IList<DTO.Article>>
            {
                Success = true,
                Content = articles
            });
        } 
        
        /// <summary>
        /// Request a list of articles.
        /// </summary>
        /// <param name="unitIds">Find articles to the listed unit ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("get-by-unit-id")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<IList<DTO.Article>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByUnitIdAsync([FromQuery]IEnumerable<long> unitIds)
        {
            var articles = await ArticleManager.GetArticlesByUnitIdAsync(unitIds);
            return Ok(new DTO.DictionaryResponse<IList<DTO.Article>>
            {
                Success = true,
                Content = articles
            });
        } 
    }
}