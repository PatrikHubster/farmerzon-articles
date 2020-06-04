using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class PersonRepository : AbstractRepository, IPersonRepository
    {
        public PersonRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Person>> GetEntitiesAsync(int? id, string userName, string normalizedUserName)
        {
            return await Context.People
                .Where(person => id == null || person.PersonId == id)
                .Where(p => userName == null || p.UserName == userName)
                .Where(p => normalizedUserName == null || p.NormalizedUserName == normalizedUserName)
                .ToListAsync();
        }

        public async Task<Person> GetPersonByArticleAsync(Article article)
        {
            var managedArticle = await Context.Articles
                .Include(a => a.Person)
                .Where(a => a.ArticleId == article.ArticleId)
                .FirstOrDefaultAsync();
            return managedArticle?.Person;
        }
    }
}