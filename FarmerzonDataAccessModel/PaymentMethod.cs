using System.Collections.Generic;

namespace FarmerzonDataAccessModel
{
    public class PaymentMethod
    {
        // primary key
        public int PaymentMethodId { get; set; }
        
        // relationships
        public IList<Order> Orders { get; set; }

        // attributes
        public string Method { get; set; }
    }
}