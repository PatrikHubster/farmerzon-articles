using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Output
{
    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType()
        {
            Name = "Address";
            
            // primary key
            Field(x => x.AddressId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.City, type: typeof(CityType));
            Field(x => x.State, type: typeof(StateType));
            Field(x => x.Country, type: typeof(CountryType));
            
            // attributes
            Field(x => x.DoorNumber, type: typeof(StringGraphType));
            Field(x => x.Street, type: typeof(StringGraphType));
        }
    }
}