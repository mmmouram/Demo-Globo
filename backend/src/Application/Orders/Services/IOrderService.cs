using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerRelationship.Application.Orders.Models;

namespace CustomerRelationship.Application.Orders.Services
{
    public interface IOrderService
    {
        Task<OrderDetailResponse> GetOrderDetailsAsync(long orderId);
        Task<List<OrderItemResponse>> GetItemsAsync(long orderId);
        Task<List<OrderItemResponse>> UpdateItemsAsync(long orderId);
        Task<byte[]> ExportItemsToExcelAsync(long orderId);

        Task<List<FiscalNoteResponse>> GetFiscalNotesAsync(long orderId);
        Task<byte[]> ExportFiscalNotesToExcelAsync(long orderId);

        Task<List<OrderBlockResponse>> GetBlocksAsync(long orderId);
        Task<List<OrderBlockResponse>> UpdateBlocksAsync(long orderId);
        Task<byte[]> ExportBlocksToExcelAsync(long orderId);

        Task<List<OrderObservationResponse>> GetObservationsAsync(long orderId);
        Task<List<OrderObservationResponse>> UpdateObservationsAsync(long orderId);
        Task<byte[]> ExportObservationsToExcelAsync(long orderId);
    }
}
