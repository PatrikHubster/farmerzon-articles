using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Output
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType()
        {
            Name = "Article";
            
            // primary key
            Field(x => x.ArticleId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Person, type: typeof(PersonType));
            Field(x => x.Unit, type: typeof(UnitType));
            Field(x => x.Orders, type: typeof(ListGraphType<ArticleForOrderType>));
            
            // attributes
            Field(x => x.Name, type: typeof(StringGraphType));
            Field(x => x.Description, type: typeof(StringGraphType));
            Field(x => x.Price, type: typeof(FloatGraphType));
            Field(x => x.Size, type: typeof(FloatGraphType));
            Field(x => x.Amount, type: typeof(IntGraphType));
            Field(x => x.UpdatedAt, type: typeof(DateTimeGraphType));
            Field(x => x.CreatedAt, type: typeof(DateTimeGraphType));
        }
    }
}