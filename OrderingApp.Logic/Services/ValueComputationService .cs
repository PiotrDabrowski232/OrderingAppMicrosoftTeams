using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Services
{
    public class ValueComputationService : IValueComputationService
    {
        private readonly OrderingDbContext _dbContext;
        public ValueComputationService(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CalculateCalories(List<OrderItemsDto> OrderItems, out int calories)
        {
            calories = OrderItems.Sum(x => x.Amount * x.Dish.Calories);
        }

        public void CalculatePrice(List<OrderItemsDto> OrderItems, out float price)
        {
            price = OrderItems.Sum(x => x.Amount * x.Dish.Price);
        }

        public float CalculateTotalOrderprice(Guid OrderId)
        {
            return _dbContext.OrderSignups
                .Where(x => x.OrderId == OrderId)
                .SelectMany(x => x.OrderItems.Select(x => x.Amount * x.Dish.Price))
                .Sum();
        }

        public float CalculateSignupTotalPrice(Guid signup)
        {
            return  _dbContext.OrderItems.Where(x => x.Signup.Id == signup)
                .Include(x => x.Dish)
                .Sum(x => x.Amount * x.Dish.Price);
        }
    }
}
