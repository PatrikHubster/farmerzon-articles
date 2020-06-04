using System.Threading.Tasks;
using FarmerzonArticlesManager.Interface;
using GraphQL.Types;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticles.GraphOutputType
{
    public class ArticleOutputType : ObjectGraphType<DTO.Article>
    {
        private IPersonManager PersonManager { get; set; }
        private IUnitManager UnitManager { get; set; }

        public ArticleOutputType(IPersonManager personManager, IUnitManager unitManager)
        {
            PersonManager = personManager;
            UnitManager = unitManager;

            Name = "Article";
            Field<NonNullGraphType<IdGraphType>>(name: "articleId");
            
            Field<NonNullGraphType<PersonOutputType>>(name: "person", resolve: LoadPerson);
            Field<NonNullGraphType<UnitOutputType>>(name: "unit", resolve: LoadUnit);
            
            Field<NonNullGraphType<StringGraphType>>(name: "name");
            Field<NonNullGraphType<StringGraphType>>(name: "description");
            Field<NonNullGraphType<FloatGraphType>>(name: "price");
            Field<NonNullGraphType<FloatGraphType>>(name: "size");
            Field<NonNullGraphType<IntGraphType>>(name: "amount");
            Field<NonNullGraphType<DateTimeGraphType>>(name: "updatedAt");
            Field<NonNullGraphType<DateTimeGraphType>>(name: "createdAt");
        }

        private async Task<DTO.Unit> LoadUnit(ResolveFieldContext<DTO.Article> context)
        {
            var article = context.Source;
            return await UnitManager.GetUnitByArticleAsync(article);
        }

        private async Task<DTO.Person> LoadPerson(ResolveFieldContext<DTO.Article> context)
        {
            var article = context.Source;
            return await PersonManager.GetPersonByArticleAsync(article);
        }
    }
}