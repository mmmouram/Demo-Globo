namespace CustomerRelationship.Domain.Orders
{
    public class OrderBlock
    {
        public string BlockType { get; set; }
        public bool IsActive { get; set; }

        public static OrderBlock Create(string blockType, bool isActive)
        {
            return new OrderBlock
            {
                BlockType = blockType,
                IsActive = isActive
            };
        }
    }
}
