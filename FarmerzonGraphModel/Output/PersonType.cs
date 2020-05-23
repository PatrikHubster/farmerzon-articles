using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Output
{
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Name = "Person";

            // primary key
            Field(x => x.PersonId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Articles, type: typeof(ListGraphType<ArticleType>));

            // attributes
            Field(x => x.NormalizedUserName, type: typeof(StringGraphType));
            Field(x => x.UserName, type: typeof(StringGraphType));
        }
    }
}