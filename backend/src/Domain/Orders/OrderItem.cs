namespace CustomerRelationship.Domain.Orders
{
    public class OrderItem
    {
        public int QuantityOrdered { get; set; }
        public int QuantityBilled { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string RelevantDates { get; set; }

        public static OrderItem Create(int qtyOrdered, int qtyBilled, decimal price, decimal discount, string dates)
        {
            return new OrderItem
            {
                QuantityOrdered = qtyOrdered,
                QuantityBilled = qtyBilled,
                Price = price,
                Discount = discount,
                RelevantDates = dates
            };
        }
    }
}
