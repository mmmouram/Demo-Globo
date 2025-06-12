using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CustomerRelationship.Application.Orders.Services;
using CustomerRelationship.Application.Orders.Models;
using CustomerRelationship.Domain.SeedWork;

namespace CustomerRelationship.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, INotification notification) : base(notification)
        {
            _orderService = orderService;
        }

        // GET: api/order/{orderId}
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetails(long orderId)
        {
            var result = await _orderService.GetOrderDetailsAsync(orderId);
            return Response(result);
        }

        // GET: api/order/{orderId}/items
        [HttpGet("{orderId}/items")]
        public async Task<IActionResult> GetOrderItems(long orderId)
        {
            var result = await _orderService.GetItemsAsync(orderId);
            return Response(result);
        }

        // PUT: api/order/{orderId}/items
        [HttpPut("{orderId}/items")]
        public async Task<IActionResult> UpdateOrderItems(long orderId)
        {
            var result = await _orderService.UpdateItemsAsync(orderId);
            return Response(result, "Items updated successfully.");
        }

        // GET: api/order/{orderId}/items/export
        [HttpGet("{orderId}/items/export")]
        public async Task<IActionResult> ExportOrderItems(long orderId)
        {
            var fileContent = await _orderService.ExportItemsToExcelAsync(orderId);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderItems.xlsx");
        }

        // GET: api/order/{orderId}/fiscalnotes
        [HttpGet("{orderId}/fiscalnotes")]
        public async Task<IActionResult> GetFiscalNotes(long orderId)
        {
            var result = await _orderService.GetFiscalNotesAsync(orderId);
            return Response(result);
        }

        // GET: api/order/{orderId}/fiscalnotes/export
        [HttpGet("{orderId}/fiscalnotes/export")]
        public async Task<IActionResult> ExportFiscalNotes(long orderId)
        {
            var fileContent = await _orderService.ExportFiscalNotesToExcelAsync(orderId);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FiscalNotes.xlsx");
        }

        // GET: api/order/{orderId}/blocks
        [HttpGet("{orderId}/blocks")]
        public async Task<IActionResult> GetOrderBlocks(long orderId)
        {
            var result = await _orderService.GetBlocksAsync(orderId);
            return Response(result);
        }

        // PUT: api/order/{orderId}/blocks
        [HttpPut("{orderId}/blocks")]
        public async Task<IActionResult> UpdateOrderBlocks(long orderId)
        {
            var result = await _orderService.UpdateBlocksAsync(orderId);
            return Response(result, "Blocks updated successfully.");
        }

        // GET: api/order/{orderId}/blocks/export
        [HttpGet("{orderId}/blocks/export")]
        public async Task<IActionResult> ExportOrderBlocks(long orderId)
        {
            var fileContent = await _orderService.ExportBlocksToExcelAsync(orderId);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderBlocks.xlsx");
        }

        // GET: api/order/{orderId}/observations
        [HttpGet("{orderId}/observations")]
        public async Task<IActionResult> GetOrderObservations(long orderId)
        {
            var result = await _orderService.GetObservationsAsync(orderId);
            return Response(result);
        }

        // PUT: api/order/{orderId}/observations
        [HttpPut("{orderId}/observations")]
        public async Task<IActionResult> UpdateOrderObservations(long orderId)
        {
            var result = await _orderService.UpdateObservationsAsync(orderId);
            return Response(result, "Observations updated successfully.");
        }

        // GET: api/order/{orderId}/observations/export
        [HttpGet("{orderId}/observations/export")]
        public async Task<IActionResult> ExportOrderObservations(long orderId)
        {
            var fileContent = await _orderService.ExportObservationsToExcelAsync(orderId);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderObservations.xlsx");
        }
    }
}
