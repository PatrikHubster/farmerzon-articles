using System.Collections.Generic;

namespace FarmerzonArticlesDataAccessModel
{
    public class Person : BaseModel
    {
        // relationships
        public IList<Article> Articles { get; set; }

        // attributes
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
    }
}