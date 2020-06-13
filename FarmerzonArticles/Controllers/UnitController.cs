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
    [Route("unit")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private IUnitManager UnitManager { get; set; }

        public UnitController(IUnitManager unitManager)
        {
            UnitManager = unitManager;
        }
        
        /// <summary>
        /// Request a list of units.
        /// </summary>
        /// <param name="unitId">Optional parameter for querying for units.</param>
        /// <param name="name">Optional parameter for querying for units.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">UnitId or Name was not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.ListResponse<DTO.Unit>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUnitsAsync([FromQuery]long? unitId, [FromQuery]string name)
        {
            var units = await UnitManager.GetEntitiesAsync(unitId, name);
            return Ok(new DTO.ListResponse<DTO.Unit>
            {
                Success = true,
                Content = units
            });
        } 
        
        /// <summary>
        /// Request a list of units.
        /// </summary>
        /// <param name="articleIds">Find units to the listed article ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("get-by-article-id")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<DTO.Unit>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUnitsByArticleIdAsync([FromQuery]IEnumerable<long> articleIds)
        {
            var units = await UnitManager.GetUnitsByArticleIdAsync(articleIds);
            return Ok(new DTO.DictionaryResponse<DTO.Unit>
            {
                Success = true,
                Content = units
            });
        } 
    }
}