using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesManager.Interface;
using GraphQL.Types;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticles.GraphOutputType
{
    public class PersonOutputType : ObjectGraphType<DTO.Person>
    {
        private IArticleManager ArticleManager { get; set; }

        public PersonOutputType(IArticleManager articleManager)
        {
            ArticleManager = articleManager;
            
            Name = "Person";
            Field<NonNullGraphType<IdGraphType>>(name: "personId");
            
            Field<NonNullGraphType<ListGraphType<ArticleOutputType>>>(name: "articles", resolve: LoadArticles);
            
            Field<NonNullGraphType<StringGraphType>>(name: "normalizedUserName");
            Field<NonNullGraphType<StringGraphType>>(name: "userName");
        }

        private async Task<IList<DTO.Article>> LoadArticles(ResolveFieldContext<DTO.Person> context)
        {
            var person = context.Source;
            return await ArticleManager.GetArticlesByPersonAsync(person);
        }
    }
}