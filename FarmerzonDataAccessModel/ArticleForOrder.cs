namespace FarmerzonDataAccessModel
{
    public class ArticleForOrder
    {
        // primary key
        public int ArticleForOrderId { get; set; }
        
        // relationships
        public Order Order { get; set; }
        public Article Article { get; set; }
        
        // attributes
        public int Amount { get; set; }
    }
}