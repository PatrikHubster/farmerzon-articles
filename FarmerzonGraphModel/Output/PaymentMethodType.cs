using FarmerzonDataAccessModel;
using GraphQL.Types;

namespace FarmerzonGraphModel.Output
{
    public class PaymentMethodType : ObjectGraphType<PaymentMethod>
    {
        public PaymentMethodType()
        {
            Name = "PaymentMethod";
            
            // primary key
            Field(x => x.PaymentMethodId, type: typeof(IdGraphType));

            // relationships
            Field(x => x.Orders, type: typeof(ListGraphType<OrderType>));
            
            // attributes
            Field(x => x.Method, type: typeof(StringGraphType));
        }
    }
}