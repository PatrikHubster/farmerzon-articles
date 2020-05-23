using System.Threading.Tasks;
using Farmerzon.Graph;
using FarmerzonDataAccess;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farmerzon.Controllers
{
    [Authorize]
    [Route("graph")]
    [ApiController]
    public class GraphController : Controller
    {
        private Query Query { get; set; }
        private Mutation Mutation { get; set; }
        
        public GraphController(Query query, Mutation mutation)
        {
            Query = query;
            Mutation = mutation;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQuery query)
        {
            var result = await new DocumentExecuter().ExecuteAsync(new ExecutionOptions
            {
                Schema = new Schema
                {
                    Query = Query,
                    Mutation = Mutation
                },
                Query = query.Query,
                OperationName = query.OperationName,
                Inputs = query.Variables.ToInputs()
            });

            if(result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}