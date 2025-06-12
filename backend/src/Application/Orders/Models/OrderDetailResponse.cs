using System.Collections.Generic;
using CustomerRelationship.Application.Common;

namespace CustomerRelationship.Application.Orders.Models
{
    public class OrderDetailResponse : BaseResponse
    {
        public long OrderId { get; set; }
        public string OrderNumber { get; set; } // This field is immutable
        public string CustomerCNPJ { get; set; }
        public string CustomerCompanyName { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
        public List<FiscalNoteResponse> FiscalNotes { get; set; } = new List<FiscalNoteResponse>();
        public List<OrderBlockResponse> Blocks { get; set; } = new List<OrderBlockResponse>();
        public List<OrderObservationResponse> Observations { get; set; } = new List<OrderObservationResponse>();
    }
}
