using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Repositories
{
    public interface IOrderDetailsRepository
    {
        OrderDetail GetOrderDetail(int orderId);
        IEnumerable<OrderItem> GetOrderItems(int orderId);
        IEnumerable<Invoice> GetInvoices(int orderId);
        IEnumerable<Observation> GetObservations(int orderId);
        IEnumerable<Block> GetBlocks(int orderId);
        void SaveUserPreferences(int orderId, UserPreferences preferences);
        UserPreferences GetUserPreferences(int orderId);
    }
}
