using CustomerRelationship.Application.Common;
using CustomerRelationship.Domain.Orders;

namespace CustomerRelationship.Application.Orders.Models
{
    public class OrderBlockResponse : BaseResponse
    {
        public string BlockType { get; set; }
        public bool IsActive { get; set; }

        public static implicit operator OrderBlockResponse(OrderBlock block)
        {
            if (block == null) return null;
            return new OrderBlockResponse
            {
                BlockType = block.BlockType,
                IsActive = block.IsActive
            };
        }
    }
}
