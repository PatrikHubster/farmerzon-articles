using System;
using System.Collections.Generic;

namespace FarmerzonDataAccessModel
{
    public class Article
    {
        // primary key
        public long ArticleId { get; set; }
        
        // relationships
        public Person Person { get; set; }
        public Unit Unit { get; set; }
        public IList<ArticleForOrder> Orders { get; set; }
        
        // attributes
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double Size { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}