using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Context
{
    public class FarmerzonContext : DbContext
    {
        public FarmerzonContext(DbContextOptions<FarmerzonContext> options) : base(options)
        {
            // nothing to do here
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleForOrder> ArticlesForOrders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}