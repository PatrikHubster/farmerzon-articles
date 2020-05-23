using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Output
{
    public class ArticleForOrderType : ObjectGraphType<ArticleForOrder>
    {
        public ArticleForOrderType()
        {
            Name = "ArticleForOrder";

            // primary key
            Field(x => x.ArticleForOrderId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Order, type: typeof(OrderType));
            Field(x => x.Article, type: typeof(ArticleType));

            // attributes
            Field(x => x.Amount, type: typeof(IntGraphType));
        }
    }
}