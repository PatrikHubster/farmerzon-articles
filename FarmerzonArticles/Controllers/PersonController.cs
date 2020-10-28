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
    [Route("person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonManager PersonManager { get; set; }

        public PersonController(IPersonManager personManager)
        {
            PersonManager = personManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.PersonOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleAsync([FromQuery] long? personId, [FromQuery] string userName,
            [FromQuery] string normalizedUserName)
        {
            var people = await PersonManager.GetEntitiesAsync(personId, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.PersonOutput>>
            {
                Success = true,
                Content = people
            });
        }

        [HttpGet("get-by-article-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, DTO.PersonOutput>>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleByArticleIdAsync([FromQuery] IEnumerable<long> articleIds)
        {
            var people = await PersonManager.GetEntitiesByArticleIdAsync(articleIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, DTO.PersonOutput>>
            {
                Success = true,
                Content = people
            });
        }
    }
}