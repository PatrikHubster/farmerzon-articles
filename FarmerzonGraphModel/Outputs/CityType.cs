using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Outputs
{
    public class CityType : ObjectGraphType<City>
    {
        public CityType()
        {
            Name = "City";
            
            // primary key
            Field(x => x.CityId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Addresses, type: typeof(ListGraphType<AddressType>));
            
            // attributes
            Field(x => x.Name, type: typeof(StringGraphType));
            Field(x => x.ZipCode, type: typeof(StringGraphType));
        }
    }
}