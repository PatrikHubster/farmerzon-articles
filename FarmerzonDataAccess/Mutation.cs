using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using FarmerzonGraphModel.Inputs;
using FarmerzonGraphModel.Outputs;
using GraphQL.Types;

namespace FarmerzonDataAccess
{
    public class Mutation : ObjectGraphType
    {
        private IUnitRepository UnitRepository { get; set; }
        public Mutation(IUnitRepository unitRepository)
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
            var foundUnits = await UnitRepository.GetEntities(null, unit.Name);
            if (foundUnits != null && foundUnits.Count > 0)
            {
                return foundUnits.First();
            }
            
            var insertedUnit = await UnitRepository.AddEntity(unit);
            return insertedUnit;
        }
    }
}