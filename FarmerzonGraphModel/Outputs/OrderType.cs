using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Outputs
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Name = "Order";
            
            // primary key
            Field(x => x.OrderId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Address, type: typeof(AddressType));
            Field(x => x.PaymentMethod, type: typeof(PaymentMethodType));
            Field(x => x.Articles, type: typeof(ListGraphType<ArticleType>));
            
            // attributes
            Field(x => x.OrderDate, type: typeof(DateTimeGraphType));
        }
    }
}