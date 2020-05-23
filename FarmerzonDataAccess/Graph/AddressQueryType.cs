using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Interface;
using FarmerzonDataAccessModel;
using FarmerzonGraphModel.Output;
using GraphQL.Types;

namespace FarmerzonDataAccess.Graph
{
    public class AddressQueryType : ObjectGraphType
    {
        private IAddressRepository AddressRepository { get; set; }
        
        public AddressQueryType(IAddressRepository addressRepository)
        {
            AddressRepository = addressRepository;
            
            Name = "AddressQuery";
            Field<ListGraphType<AddressType>>(
                name: "addresses",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "addressId"},
                    new QueryArgument<StringGraphType> {Name = "doorNumber"},
                    new QueryArgument<StringGraphType> {Name = "street"}
                },
                resolve: GetAddresses);
        }
        
        private async Task<IList<Address>> GetAddresses(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("addressId");
            var doorNumber = context.GetArgument<string>("doorNumber");
            var street = context.GetArgument<string>("street");
            return await AddressRepository.GetEntitiesAsync(id, doorNumber, street);
        }
    }
}