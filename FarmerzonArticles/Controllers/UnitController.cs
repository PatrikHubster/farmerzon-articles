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

        [HttpPost]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.UnitOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUnitAsync([FromBody] DTO.UnitInput unit)
        {
            var insertedUnit = await UnitManager.InsertEntityAsync(unit);
            return Ok(new DTO.SuccessResponse<DTO.UnitOutput>
            {
                Success = true,
                Content = insertedUnit
            });
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.UnitOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUnitsAsync([FromQuery] long? unitId, [FromQuery] string name)
        {
            var units = await UnitManager.GetEntitiesAsync(unitId, name);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.UnitOutput>>
            {
                Success = true,
                Content = units
            });
        }

        [HttpGet("get-by-article-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, DTO.UnitOutput>>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUnitsByArticleIdAsync([FromQuery] IEnumerable<long> articleIds)
        {
            var units = await UnitManager.GetEntitiesByArticleIdAsync(articleIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, DTO.UnitOutput>>
            {
                Success = true,
                Content = units
            });
        }

        [HttpPut]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.UnitOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUnitAsync([FromQuery] long unitId, [FromBody] DTO.UnitInput unit)
        {
            var updatedUnit = await UnitManager.UpdateEntityAsync(unitId, unit);
            return Ok(new DTO.SuccessResponse<DTO.UnitOutput>
            {
                Success = true,
                Content = updatedUnit
            });
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.UnitOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUnitAsync([FromQuery] long unitId)
        {
            var deletedUnit = await UnitManager.RemoveEntityByIdAsync(unitId);
            return Ok(new DTO.SuccessResponse<DTO.UnitOutput>
            {
                Success = true,
                Content = deletedUnit
            });
        }
    }
}