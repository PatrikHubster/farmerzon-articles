using System.Collections.Generic;

namespace FarmerzonArticlesDataAccessModel
{
    public class Unit
    {
        // primary key
        public long UnitId { get; set; }
        
        // relationships
        public IList<Article> Articles { get; set; }
        
        // attributes
        public string Name { get; set; }
    }
}