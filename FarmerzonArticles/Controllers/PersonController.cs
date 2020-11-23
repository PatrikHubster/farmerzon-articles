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