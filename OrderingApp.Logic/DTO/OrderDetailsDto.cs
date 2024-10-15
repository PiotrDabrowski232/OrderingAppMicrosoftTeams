using OrderingApp.Data.Models.Enum;

namespace OrderingApp.Logic.DTO
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string RestaurantName { get; set; }
        public RestauranType RestaurantType { get; set; }
        public bool MyOrder { get; set; }

    }
}
