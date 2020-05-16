using System.Threading.Tasks;
using Farmerzon.Graph;
using FarmerzonDataAccess;
using FarmerzonDataAccess.Context;
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
        private FarmerzonContext Context { get; set; }

        public GraphController(FarmerzonContext context)
        {
            Context = context;
        }
        
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] GraphQuery query)
        {
            var result = await new DocumentExecuter().ExecuteAsync(new ExecutionOptions
            {
                Schema = new Schema
                {
                    Query = new Query(Context),
                    Mutation = new Mutation(Context)
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