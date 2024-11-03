using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Services.Interface
{
    public interface IValueComputationService
    {
        public void CalculatePrice(List<OrderItemsDto> OrderItems, out float price);
        public void CalculateCalories(List<OrderItemsDto> OrderItems, out int calories);
        public float CalculateTotalOrderprice(Guid OrderId);
        public float CalculateSignupTotalPrice(Guid signup);
    }
}
