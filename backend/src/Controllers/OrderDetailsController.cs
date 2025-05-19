using Microsoft.AspNetCore.Mvc;
using MyApp.Services;
using MyApp.Models;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IExportService _exportService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService, IExportService exportService)
        {
            _orderDetailsService = orderDetailsService;
            _exportService = exportService;
        }

        // GET: api/OrderDetails/{orderId}
        [HttpGet("{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            try
            {
                var orderDetail = _orderDetailsService.GetOrderDetails(orderId);
                if (orderDetail == null)
                {
                    return NotFound(new { Message = "Order not found" });
                }
                return Ok(orderDetail);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Error retrieving order details", Error = ex.Message });
            }
        }

        // GET: api/OrderDetails/{orderId}/update-section?section={section}
        [HttpGet("{orderId}/update-section")]
        public IActionResult UpdateSection(int orderId, [FromQuery] string section)
        {
            try
            {
                var updatedData = _orderDetailsService.UpdateSection(orderId, section);
                return Ok(updatedData);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Error updating section", Error = ex.Message });
            }
        }

        // GET: api/OrderDetails/{orderId}/export?section={section}
        [HttpGet("{orderId}/export")]
        public IActionResult ExportSectionToExcel(int orderId, [FromQuery] string section)
        {
            try
            {
                var excelFile = _exportService.ExportSectionToExcel(orderId, section);
                if (excelFile == null)
                {
                    return StatusCode(500, new { Message = "Error exporting data to Excel" });
                }
                return File(excelFile.Content, excelFile.ContentType, excelFile.FileName);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Error exporting section", Error = ex.Message });
            }
        }

        // POST: api/OrderDetails/{orderId}/preferences
        [HttpPost("{orderId}/preferences")]
        public IActionResult SaveUserPreferences(int orderId, [FromBody] UserPreferences preferences)
        {
            try
            {
                _orderDetailsService.SaveUserPreferences(orderId, preferences);
                return Ok(new { Message = "Preferences saved successfully" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Error saving preferences", Error = ex.Message });
            }
        }

        // GET: api/OrderDetails/{orderId}/preferences
        [HttpGet("{orderId}/preferences")]
        public IActionResult GetUserPreferences(int orderId)
        {
            try
            {
                var preferences = _orderDetailsService.GetUserPreferences(orderId);
                return Ok(preferences);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Error retrieving preferences", Error = ex.Message });
            }
        }
    }
}
