using MyApp.Models;
using MyApp.Repositories;
using System;

namespace MyApp.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public OrderDetail GetOrderDetails(int orderId)
        {
            // Retrieve order details from repository
            var orderDetail = _orderDetailsRepository.GetOrderDetail(orderId);
            return orderDetail;
        }

        public object UpdateSection(int orderId, string section)
        {
            // Depending on the section, update and return the latest data
            switch (section.ToLower())
            {
                case "orderitems":
                    return _orderDetailsRepository.GetOrderItems(orderId);
                case "invoices":
                    return _orderDetailsRepository.GetInvoices(orderId);
                case "observations":
                    return _orderDetailsRepository.GetObservations(orderId);
                case "blocks":
                    return _orderDetailsRepository.GetBlocks(orderId);
                default:
                    throw new ArgumentException("Invalid section");
            }
        }

        public void SaveUserPreferences(int orderId, UserPreferences preferences)
        {
            _orderDetailsRepository.SaveUserPreferences(orderId, preferences);
        }

        public UserPreferences GetUserPreferences(int orderId)
        {
            return _orderDetailsRepository.GetUserPreferences(orderId);
        }
    }
}
