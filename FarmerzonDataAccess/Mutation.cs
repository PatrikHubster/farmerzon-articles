using System;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Implementations;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using FarmerzonGraphModel.Inputs;
using FarmerzonGraphModel.Outputs;
using GraphQL.Types;

namespace FarmerzonDataAccess
{
    public class Mutation : ObjectGraphType
    {
        private FarmerzonContext Context { get; set; }
        private IUnitRepository UnitRepository { get; set; }
        public Mutation(FarmerzonContext context)
        {
            Context = context;
            UnitRepository = new UnitRepository();

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
            await using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    var foundUnits = await UnitRepository.GetEntities(null, unit.Name, Context);
                    if (foundUnits != null && foundUnits.Count > 0)
                    {
                        return foundUnits.First();
                    }
                    
                    var insertedUnit = await UnitRepository.AddEntity(unit, Context);
                    transaction.Commit();
                    return insertedUnit;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
    }
}