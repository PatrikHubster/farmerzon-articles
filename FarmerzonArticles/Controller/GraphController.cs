using System.Threading.Tasks;
using FarmerzonArticles.GraphControllerType;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerzonArticles.Controller
{
    [Authorize]
    [Route("graph")]
    [ApiController]
    public class GraphController : Microsoft.AspNetCore.Mvc.Controller
    {
        private ISchema Schema { get; set; }
        private IDocumentExecuter Executer { get; set; }
        
        public GraphController(ISchema schema, IDocumentExecuter executer)
        {
            Schema = schema;
            Executer = executer;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQuery query)
        {
            var result = await Executer.ExecuteAsync(options =>
            {
                options.Schema = Schema;
                options.Query = query.Query;
            }).ConfigureAwait(false);

            if(result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }
            
            return Ok(result.Data);
        }
    }
}