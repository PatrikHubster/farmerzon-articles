using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesManager.Interface;
using GraphQL.DataLoader;
using GraphQL.Types;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticles.GraphOutputType
{
    public class UnitOutputType : ObjectGraphType<DTO.Unit>
    {
        private IDataLoaderContextAccessor Accessor { get; set; }
        private IArticleManager ArticleManager { get; set; }

        private void InitDependencies(IDataLoaderContextAccessor accessor, IArticleManager articleManager)
        {
            Accessor = accessor;
            ArticleManager = articleManager;            
        }

        private void InitType()
        {
            Name = "Unit";
            Field<IdGraphType, long>(name: "unitId");
            
            Field<ListGraphType<ArticleOutputType>, IEnumerable<DTO.Article>>()
                .Name("articles")
                .ResolveAsync(LoadArticles);
            
            Field<StringGraphType, string>().Name("name");            
        }

        public UnitOutputType(IDataLoaderContextAccessor accessor, IArticleManager articleManager)
        {
            InitDependencies(accessor, articleManager);
            InitType();
        }
        
        private Task<IEnumerable<DTO.Article>> LoadArticles(ResolveFieldContext<DTO.Unit> context)
        {
            var loader =
                Accessor.Context.GetOrAddCollectionBatchLoader<long, DTO.Article>("GetArticlesByUnitId",
                    ArticleManager.GetArticlesByUnitIdAsync);
            return loader.LoadAsync(context.Source.UnitId);
        }
    }
}