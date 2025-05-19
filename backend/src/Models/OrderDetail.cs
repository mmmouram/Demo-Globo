using System.Collections.Generic;

namespace MyApp.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Client Client { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<Observation> Observations { get; set; }
        public IEnumerable<Block> Blocks { get; set; }
    }
}
