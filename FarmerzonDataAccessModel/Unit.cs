using System.Collections.Generic;

namespace FarmerzonDataAccessModel
{
    public class Unit
    {
        // primary key
        public int UnitId { get; set; }
        
        // relationships
        public IList<Article> Articles { get; set; }
        
        // attributes
        public string Name { get; set; }
    }
}