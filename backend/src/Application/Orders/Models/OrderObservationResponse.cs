using CustomerRelationship.Application.Common;
using CustomerRelationship.Domain.Orders;

namespace CustomerRelationship.Application.Orders.Models
{
    public class OrderObservationResponse : BaseResponse
    {
        public string OperationType { get; set; }
        public string Text { get; set; }

        public static implicit operator OrderObservationResponse(OrderObservation observation)
        {
            if (observation == null) return null;
            return new OrderObservationResponse
            {
                OperationType = observation.OperationType,
                Text = observation.Text
            };
        }
    }
}
