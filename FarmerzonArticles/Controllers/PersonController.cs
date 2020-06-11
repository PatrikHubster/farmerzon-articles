using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesManager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticles.Controllers
{
    [Route("person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonManager PersonManager { get; set; }

        public PersonController(IPersonManager personManager)
        {
            PersonManager = personManager;
        }
        
        /// <summary>
        /// Request a list of people.
        /// </summary>
        /// <param name="personId">Optional parameter for querying for people.</param>
        /// <param name="userName">Optional parameter for querying for people.</param>
        /// <param name="normalizedUserName">Optional parameter for querying for people.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">PeopleId, userName or normalizedUserName was not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.ListResponse<DTO.Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleAsync([FromQuery]long? personId, [FromQuery]string userName,
            [FromQuery] string normalizedUserName)
        {
            var people = await PersonManager.GetEntitiesAsync(personId, userName, normalizedUserName);
            return Ok(new DTO.ListResponse<DTO.Person>
            {
                Success = true,
                Content = people
            });
        }
        
        /// <summary>
        /// Request a list of people.
        /// </summary>
        /// <param name="articleIds">Find people to the listed article ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("get-by-article-id")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<DTO.Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleByArticleIdAsync([FromQuery]IEnumerable<long> articleIds)
        {
            var people = await PersonManager.GetPeopleByArticleIdAsync(articleIds);
            return Ok(new DTO.DictionaryResponse<DTO.Person>
            {
                Success = true,
                Content = people
            });
        } 
    }
}