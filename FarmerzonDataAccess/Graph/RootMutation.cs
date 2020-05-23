using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Interface;
using FarmerzonDataAccessModel;
using FarmerzonGraphModel.Input;
using FarmerzonGraphModel.Output;
using GraphQL.Types;

namespace FarmerzonDataAccess.Graph
{
    public class RootMutation : ObjectGraphType
    {
        private IUnitRepository UnitRepository { get; set; }
        public RootMutation(IUnitRepository unitRepository)
        {
            UnitRepository = unitRepository;

            Field<UnitType>(
                "createUnit",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UnitInputType>> {Name = "unit"}
                ),
                resolve: AddUnit);
        }

        private async Task<Unit> AddUnit(ResolveFieldContext<object> context)
        {
            var unit = context.GetArgument<Unit>("unit");
            var foundUnits = await UnitRepository.GetEntitiesAsync(null, unit.Name);
            if (foundUnits != null && foundUnits.Count > 0)
            {
                return foundUnits.First();
            }
            
            var insertedUnit = await UnitRepository.AddEntityAsync(unit);
            return insertedUnit;
        }
    }
}