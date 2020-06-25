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
        /// <param name="expirationDate">Optional parameter for querying for articles.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IList<DTO.ArticleResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleAsync([FromQuery]long? articleId, [FromQuery]string name,
            [FromQuery]string description, [FromQuery]double? price, [FromQuery]int? amount, [FromQuery]double? size,
            [FromQuery]DateTime? createdAt, [FromQuery]DateTime? updatedAt, [FromQuery]DateTime? expirationDate)
        {
            var articles = await ArticleManager.GetEntitiesAsync(articleId, name, description, price, 
                amount, size, createdAt, updatedAt, expirationDate);
            return Ok(new DTO.SuccessResponse<IList<DTO.ArticleResponse>>
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
        [Authorize]
        [HttpGet("get-by-normalized-user-name")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IList<DTO.ArticleResponse>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByPersonIdAsync([FromQuery]IEnumerable<string> normalizedUserNames)
        {
            var articles = await ArticleManager.GetArticlesByNormalizedUserNameAsync(normalizedUserNames);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IList<DTO.ArticleResponse>>>
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
        [Authorize]
        [HttpGet("get-by-unit-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IList<DTO.ArticleResponse>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByUnitIdAsync([FromQuery]IEnumerable<long> unitIds)
        {
            var articles = await ArticleManager.GetArticlesByUnitIdAsync(unitIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IList<DTO.ArticleResponse>>>
            {
                Success = true,
                Content = articles
            });
        }
        
        /// <summary>
        /// Request a list of articles.
        /// </summary>
        /// <param name="amount">Find a specific amount of articles which expire soon.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("get-by-expiration-date")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IList<DTO.ArticleResponse>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArticlesByExpirationDate([FromQuery]int amount)
        {
            var articles = await ArticleManager.GetArticlesByExpirationDate(amount);
            return Ok(new DTO.SuccessResponse<IList<DTO.ArticleResponse>>
            {
                Success = true,
                Content = articles
            });
        }

        /// <summary>
        /// Posts a single article for a person.
        /// </summary>
        /// <param name="articleInput">New article to insert into database.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Post was able to execute.</response>
        /// <response code="400">Article was invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.ArticleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddArticle([FromBody] DTO.ArticleInput articleInput)
        {
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst("userName")?.Value;

            var articleResponse = await ArticleManager.AddArticle(articleInput, normalizedUserName, userName);   
            return Ok(new DTO.SuccessResponse<DTO.ArticleResponse>
            {
                Success = true,
                Content = articleResponse
            });
        }
    }
}