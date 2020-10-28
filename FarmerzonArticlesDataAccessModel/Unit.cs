using System.Collections.Generic;

namespace FarmerzonArticlesDataAccessModel
{
    public class Unit : BaseModel
    {
        // relationships
        public virtual IList<Article> Articles { get; set; }
        
        // attributes
        public string Name { get; set; }
    }
}