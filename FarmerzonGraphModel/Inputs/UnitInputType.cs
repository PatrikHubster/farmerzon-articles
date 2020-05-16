using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Inputs
{
    public class UnitInputType : InputObjectGraphType<Unit>
    {
        public UnitInputType()
        {
            Name = "Unit";
            
            Field(x => x.Name, type: typeof(StringGraphType), nullable: false);
        }
    }
}