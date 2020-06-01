using System.Collections.Generic;

namespace FarmerzonArticlesDataAccessModel
{
    public class Person
    {
        // primary key
        public int PersonId { get; set; }
        
        // relationships
        public IList<Article> Articles { get; set; }

        // attributes
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
    }
}