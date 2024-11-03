using Microsoft.Graph.Models;
using OrderingApp.Data.Models.Enum;

namespace OrderingApp.Logic.DTO
{
    public class OrderInformationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float MinValue { get; set; }
        public float DeliveryCost { get; set; }
        public float FreeDeliveryFrom { get; set; }
        public int PhoneNumber { get; set; }
        public decimal BankAccountNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string RestaurantName { get; set; }
        public RestauranType RestaurantType { get; set; }
        public string Author { get; set; }
        public bool Myorder { get; set; }
        public bool IsSignedUp { get; set; }
        public OrderStatus Status { get; set; }

        public IEnumerable<OrderSignupsDto>? Signups { get; set; }
    }
}
