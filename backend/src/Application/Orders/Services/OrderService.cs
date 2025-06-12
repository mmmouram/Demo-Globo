using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRelationship.Application.Orders.Models;
using CustomerRelationship.Application.Common;
using CustomerRelationship.Domain.SeedWork;
using CustomerRelationship.Domain.Orders;
using CustomerRelationship.Infra.Data.Orders.Repository;
using ClosedXML.Excel;
using System.IO;

namespace CustomerRelationship.Application.Orders.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(INotification notification, IOrderRepository orderRepository)
            : base(notification)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDetailResponse> GetOrderDetailsAsync(long orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                var response = new OrderDetailResponse
                {
                    OrderId = order.Id,
                    OrderNumber = order.OrderNumber,
                    CustomerCNPJ = order.CustomerCNPJ,
                    CustomerCompanyName = order.CustomerCompanyName,
                    Items = order.Items?.Select(i => (OrderItemResponse)i).ToList() ?? new List<OrderItemResponse>(),
                    FiscalNotes = order.FiscalNotes?.Select(fn => (FiscalNoteResponse)fn).ToList() ?? new List<FiscalNoteResponse>(),
                    Blocks = order.Blocks?.Select(b => (OrderBlockResponse)b).ToList() ?? new List<OrderBlockResponse>(),
                    Observations = order.Observations?.Select(o => (OrderObservationResponse)o).ToList() ?? new List<OrderObservationResponse>()
                };
                return response;
            }
            catch (Exception ex)
            {
                Notify("Error retrieving order details: " + ex.Message);
                return null;
            }
        }

        public async Task<List<OrderItemResponse>> GetItemsAsync(long orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                return order.Items?.Select(i => (OrderItemResponse)i).ToList() ?? new List<OrderItemResponse>();
            }
            catch (Exception ex)
            {
                Notify("Error retrieving order items: " + ex.Message);
                return null;
            }
        }

        public async Task<List<OrderItemResponse>> UpdateItemsAsync(long orderId)
        {
            try
            {
                // For simplicity, updating items means reloading from the database
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                return order.Items?.Select(i => (OrderItemResponse)i).ToList() ?? new List<OrderItemResponse>();
            }
            catch (Exception ex)
            {
                Notify("Error updating order items: " + ex.Message);
                return null;
            }
        }

        public async Task<byte[]> ExportItemsToExcelAsync(long orderId)
        {
            var items = await GetItemsAsync(orderId);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Items");
                worksheet.Cell(1, 1).Value = "QuantityOrdered";
                worksheet.Cell(1, 2).Value = "QuantityBilled";
                worksheet.Cell(1, 3).Value = "Price";
                worksheet.Cell(1, 4).Value = "Discount";
                worksheet.Cell(1, 5).Value = "RelevantDates";
                int row = 2;
                foreach (var item in items)
                {
                    worksheet.Cell(row, 1).Value = item.QuantityOrdered;
                    worksheet.Cell(row, 2).Value = item.QuantityBilled;
                    worksheet.Cell(row, 3).Value = item.Price;
                    worksheet.Cell(row, 4).Value = item.Discount;
                    worksheet.Cell(row, 5).Value = item.RelevantDates;
                    row++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public async Task<List<FiscalNoteResponse>> GetFiscalNotesAsync(long orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                return order.FiscalNotes?.Select(fn => (FiscalNoteResponse)fn).ToList() ?? new List<FiscalNoteResponse>();
            }
            catch (Exception ex)
            {
                Notify("Error retrieving fiscal notes: " + ex.Message);
                return null;
            }
        }

        public async Task<byte[]> ExportFiscalNotesToExcelAsync(long orderId)
        {
            var notes = await GetFiscalNotesAsync(orderId);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("FiscalNotes");
                worksheet.Cell(1, 1).Value = "Code";
                worksheet.Cell(1, 2).Value = "Series";
                worksheet.Cell(1, 3).Value = "EmissionDate";
                worksheet.Cell(1, 4).Value = "Value";
                int row = 2;
                foreach (var note in notes)
                {
                    worksheet.Cell(row, 1).Value = note.Code;
                    worksheet.Cell(row, 2).Value = note.Series;
                    worksheet.Cell(row, 3).Value = note.EmissionDate;
                    worksheet.Cell(row, 4).Value = note.Value;
                    row++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public async Task<List<OrderBlockResponse>> GetBlocksAsync(long orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                return order.Blocks?.Select(b => (OrderBlockResponse)b).ToList() ?? new List<OrderBlockResponse>();
            }
            catch (Exception ex)
            {
                Notify("Error retrieving order blocks: " + ex.Message);
                return null;
            }
        }

        public async Task<List<OrderBlockResponse>> UpdateBlocksAsync(long orderId)
        {
            try
            {
                // For simplicity, updating blocks means reloading from the database
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                return order.Blocks?.Select(b => (OrderBlockResponse)b).ToList() ?? new List<OrderBlockResponse>();
            }
            catch (Exception ex)
            {
                Notify("Error updating order blocks: " + ex.Message);
                return null;
            }
        }

        public async Task<byte[]> ExportBlocksToExcelAsync(long orderId)
        {
            var blocks = await GetBlocksAsync(orderId);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blocks");
                worksheet.Cell(1, 1).Value = "BlockType";
                worksheet.Cell(1, 2).Value = "IsActive";
                int row = 2;
                foreach (var block in blocks)
                {
                    worksheet.Cell(row, 1).Value = block.BlockType;
                    worksheet.Cell(row, 2).Value = block.IsActive ? "Active" : "Inactive";
                    row++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public async Task<List<OrderObservationResponse>> GetObservationsAsync(long orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                return order.Observations?.Select(o => (OrderObservationResponse)o).ToList() ?? new List<OrderObservationResponse>();
            }
            catch (Exception ex)
            {
                Notify("Error retrieving order observations: " + ex.Message);
                return null;
            }
        }

        public async Task<List<OrderObservationResponse>> UpdateObservationsAsync(long orderId)
        {
            try
            {
                // For simplicity, updating observations means reloading from the database
                var order = await _orderRepository.GetByIDAsync(orderId);
                if (order == null)
                {
                    Notify("Order not found.");
                    return null;
                }
                return order.Observations?.Select(o => (OrderObservationResponse)o).ToList() ?? new List<OrderObservationResponse>();
            }
            catch (Exception ex)
            {
                Notify("Error updating order observations: " + ex.Message);
                return null;
            }
        }

        public async Task<byte[]> ExportObservationsToExcelAsync(long orderId)
        {
            var observations = await GetObservationsAsync(orderId);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Observations");
                worksheet.Cell(1, 1).Value = "OperationType";
                worksheet.Cell(1, 2).Value = "Text";
                int row = 2;
                foreach (var obs in observations)
                {
                    worksheet.Cell(row, 1).Value = obs.OperationType;
                    worksheet.Cell(row, 2).Value = obs.Text;
                    row++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
