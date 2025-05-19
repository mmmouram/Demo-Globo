using MyApp.Models;

namespace MyApp.Services
{
    public interface IOrderDetailsService
    {
        OrderDetail GetOrderDetails(int orderId);
        object UpdateSection(int orderId, string section);
        void SaveUserPreferences(int orderId, UserPreferences preferences);
        UserPreferences GetUserPreferences(int orderId);
    }
}
