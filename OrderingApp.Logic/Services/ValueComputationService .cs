using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Services
{
    public class ValueComputationService : IValueComputationService
    {
        public void CalculateCalories(List<OrderItemsDto> OrderItems, out int calories)
        {
            calories = OrderItems.Sum(x => x.Amount * x.Dish.Calories);
        }

        public void CalculatePrice(List<OrderItemsDto> OrderItems, out float price)
        {
            price = OrderItems.Sum(x => x.Amount * x.Dish.Price);
        }
    }
}
