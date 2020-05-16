using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Outputs
{
    public class StateType : ObjectGraphType<State>
    {
        public StateType()
        {
            Name = "State";
            
            // primary key
            Field(x => x.StateId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Addresses, type: typeof(ListGraphType<AddressType>));
            
            // attributes
            Field(x => x.Name, type: typeof(StringGraphType));
        }
    }
}