using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess
{
    public class FarmerzonArticlesContext : DbContext
    {
        public FarmerzonArticlesContext(DbContextOptions<FarmerzonArticlesContext> options) : base(options)
        {
            // nothing to do here
        }
        
        public DbSet<Article> Articles { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}