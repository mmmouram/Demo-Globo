namespace CustomerRelationship.Domain.Orders
{
    public class OrderObservation
    {
        public string OperationType { get; set; }
        public string Text { get; set; }

        public static OrderObservation Create(string operationType, string text)
        {
            return new OrderObservation
            {
                OperationType = operationType,
                Text = text
            };
        }
    }
}
