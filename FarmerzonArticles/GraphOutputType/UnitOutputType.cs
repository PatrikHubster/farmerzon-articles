using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesManager.Interface;
using GraphQL.Types;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticles.GraphOutputType
{
    public class UnitOutputType : ObjectGraphType<DTO.Unit>
    {
        private IArticleManager ArticleManager { get; set; }

        public UnitOutputType(IArticleManager articleManager)
        {
            ArticleManager = articleManager;
            
            Name = "Unit";
            Field<NonNullGraphType<IdGraphType>>(name: "unitId");
            
            Field<NonNullGraphType<ListGraphType<ArticleOutputType>>>(name: "articles", resolve: LoadArticles);
            
            Field<NonNullGraphType<StringGraphType>>(name: "name");
        }

        private async Task<IList<DTO.Article>> LoadArticles(ResolveFieldContext<DTO.Unit> context)
        {
            var unit = context.Source;
            return await ArticleManager.GetArticlesByUnitAsync(unit);
        }
    }
}