using OrderingApp.Data.Models.Enum;

namespace OrderingApp.Logic.DTO
{
    public class OrderBasicInfoDto
    {
        public Guid Id { get; set; }
        public float MinValue { get; set; }
        public float DeliveryCost { get; set; }
        public int OrderSignupCount { get; set; }
        public float FreeDeliveryFrom { get; set; }
        public OrderStatus Status { get; set; }
    }
}
