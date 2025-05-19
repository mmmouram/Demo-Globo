using MyApp.Models;
using MyApp.Config;
using System.Linq;
using System.Collections.Generic;

namespace MyApp.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly AppDbContext _context;

        public OrderDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public OrderDetail GetOrderDetail(int orderId)
        {
            return _context.OrderDetails.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<OrderItem> GetOrderItems(int orderId)
        {
            return _context.OrderItems.Where(i => i.OrderId == orderId).ToList();
        }

        public IEnumerable<Invoice> GetInvoices(int orderId)
        {
            return _context.Invoices.Where(i => i.OrderId == orderId).ToList();
        }

        public IEnumerable<Observation> GetObservations(int orderId)
        {
            return _context.Observations.Where(o => o.OrderId == orderId).ToList();
        }

        public IEnumerable<Block> GetBlocks(int orderId)
        {
            return _context.Blocks.Where(b => b.OrderId == orderId).ToList();
        }

        public void SaveUserPreferences(int orderId, UserPreferences preferences)
        {
            var existing = _context.UserPreferences.FirstOrDefault(p => p.OrderId == orderId);
            if (existing != null)
            {
                existing.PreferencesJson = preferences.PreferencesJson;
                _context.UserPreferences.Update(existing);
            }
            else
            {
                preferences.OrderId = orderId;
                _context.UserPreferences.Add(preferences);
            }
            _context.SaveChanges();
        }

        public UserPreferences GetUserPreferences(int orderId)
        {
            return _context.UserPreferences.FirstOrDefault(p => p.OrderId == orderId);
        }
    }
}
