using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Output
{
    public class UnitType : ObjectGraphType<Unit>
    {
        public UnitType()
        {
            Name = "Unit";
            
            // primary key
            Field(x => x.UnitId, type: typeof(IdGraphType));
            
            // relationships
            Field(x => x.Articles, type: typeof(ListGraphType<ArticleType>));
            
            // attributes
            Field(x => x.Name, type: typeof(StringGraphType));
        }
    }
}