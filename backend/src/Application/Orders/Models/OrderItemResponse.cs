using CustomerRelationship.Application.Common;
using CustomerRelationship.Domain.Orders;

namespace CustomerRelationship.Application.Orders.Models
{
    public class OrderItemResponse : BaseResponse
    {
        public int QuantityOrdered { get; set; }
        public int QuantityBilled { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string RelevantDates { get; set; }  // Placeholder for relevant dates

        public static implicit operator OrderItemResponse(OrderItem item)
        {
            if (item == null) return null;
            return new OrderItemResponse
            {
                QuantityOrdered = item.QuantityOrdered,
                QuantityBilled = item.QuantityBilled,
                Price = item.Price,
                Discount = item.Discount,
                RelevantDates = item.RelevantDates
            };
        }
    }
}
