using FarmerzonDataAccess.Graph;
using GraphQL;
using GraphQL.Types;

namespace Farmerzon.Graph
{
    public class RootSchema : Schema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            Mutation = resolver.Resolve<RootMutation>();
        }
    }
}