using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Output
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Name = "Country";
            
            // primary key
            Field(x => x.CountryId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Addresses, type: typeof(ListGraphType<AddressType>));
            
            // attributes
            Field(x => x.Name, type: typeof(StringGraphType));
            Field(x => x.Code, type: typeof(StringGraphType));
        }
    }
}