using System;
using System.Collections.Generic;

namespace FarmerzonDataAccessModel
{
    public class Order
    {
        // primary key
        public int OrderId { get; set; }
        
        // relationships
        public Address Address { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IList<ArticleForOrder> Articles { get; set; }

        // attributes
        public DateTime OrderDate { get; set; }
    }
}