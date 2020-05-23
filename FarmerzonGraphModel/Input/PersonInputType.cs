using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Input
{
    public class PersonInputType : InputObjectGraphType<Person>
    {
        public PersonInputType()
        {
            Name = "Person";
            Field(x => x.PersonId, type: typeof(IdGraphType), nullable: true);
            Field(x => x.NormalizedUserName, type: typeof(StringGraphType), nullable: true);
            Field(x => x.UserName, type: typeof(StringGraphType), nullable: true);
        }
    }
}