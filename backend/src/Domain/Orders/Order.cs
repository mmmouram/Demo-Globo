using System.Collections.Generic;

namespace CustomerRelationship.Domain.Orders
{
    public class Order
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerCNPJ { get; set; }
        public string CustomerCompanyName { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public List<FiscalNote> FiscalNotes { get; set; } = new List<FiscalNote>();
        public List<OrderBlock> Blocks { get; set; } = new List<OrderBlock>();
        public List<OrderObservation> Observations { get; set; } = new List<OrderObservation>();

        public static Order Create(string orderNumber, string customerCNPJ, string customerCompanyName)
        {
            return new Order
            {
                OrderNumber = orderNumber,
                CustomerCNPJ = customerCNPJ,
                CustomerCompanyName = customerCompanyName
            };
        }
    }
}
